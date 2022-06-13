using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveSceneManager : MonoBehaviour
{
    public Animator FadeToBlack;
    public static ActiveSceneManager _Current;
    public string ImportantObjectsScene;
    public string CurrentlyLoadedScene;
    public Vector3 RespawnLocation;
    public string RespawnToScene;
    private void Awake()
    {
        if(_Current!= this)
        {
            _Current = this;
        }
    }

    private void Start()
    {
        GameEventsSystem.gameEventsSystem.AreaEvents.RestartSceneEvent += ReloadScene;
        GameEventsSystem.gameEventsSystem.AreaEvents.LoadAreaEvent += LoadArea;
        GameEventsSystem.gameEventsSystem.AreaEvents.UnloadAreaEvent += UnloadScene;

        RespawnLocation = PlayerStats._CurrentPlayerStats.gameObject.transform.position;

        if (!SceneManager.GetSceneByName(CurrentlyLoadedScene).isLoaded)
        {
            //SceneManager.LoadScene(CurrentlyLoadedScene, LoadSceneMode.Additive); //loading initial scene
            GameEventsSystem.gameEventsSystem.AreaEvents.OnLoadArea(CurrentlyLoadedScene);
        }
    }

    public void LoadArea(string AreaToLoad)
    {
        if(!Application.CanStreamedLevelBeLoaded(AreaToLoad))
        {
            Debug.Log("Invalid Scene Name");
            return;
        }
        if (SceneManager.GetSceneByName(AreaToLoad).isLoaded)
        {
            return;
        }
        //SceneManager.LoadSceneAsync(AreaToLoad,LoadSceneMode.Additive);
        StartCoroutine(LoadCoroutine(AreaToLoad));
        this.CurrentlyLoadedScene = AreaToLoad;
    }

    public void ReloadScene(string SceneToReload)
    {
        if(SceneManager.GetSceneByName(SceneToReload).isLoaded)
        {
            SceneManager.UnloadSceneAsync(SceneToReload);
            SceneManager.LoadSceneAsync(SceneToReload, LoadSceneMode.Additive);
        }
    }


    public void UnloadScene(string SceneToUnload)
    {


        if (SceneManager.GetSceneByName(SceneToUnload).isLoaded)
        {
            SceneManager.UnloadSceneAsync(SceneToUnload);
        }
    }


    IEnumerator LoadCoroutine(string SCENEID)
    {
        AsyncOperation LoadOp = SceneManager.LoadSceneAsync(SCENEID, LoadSceneMode.Additive);
        InputManager.GameIsPaused = true;
        while(!LoadOp.isDone)
        {
            yield return null;
        }
        Time.timeScale = 1f;
        InputManager.GameIsPaused = false;
        yield break;
    }


    IEnumerator UnloadCoroutine()
    {
        
        yield break;
    }

    IEnumerator ReloadCoroutine()
    {
        yield break;
    }
}
