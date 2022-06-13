using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneMisc : MonoBehaviour
{
    public string SceneToLoad = "GoldBaseScene";
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
