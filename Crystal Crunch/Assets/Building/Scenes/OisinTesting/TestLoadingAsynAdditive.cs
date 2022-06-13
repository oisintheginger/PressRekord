using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TestLoadingAsynAdditive : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetSceneByName(sceneToLoad).isLoaded)
        {
            return;
        }

        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
                
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SceneManager.UnloadSceneAsync(sceneToLoad);
    }
}
