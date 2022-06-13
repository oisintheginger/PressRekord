using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeleportToLocationButton : MonoBehaviour
{
    public string SceneToLoad;
    private void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(() => Clicked());
    }
    void Clicked()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
