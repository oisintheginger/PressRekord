using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuFunctions : MonoBehaviour
{
    FMOD.Studio.EventInstance Event,train,robot;
    public void Start()
    {
        Event = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
        train = FMODUnity.RuntimeManager.CreateInstance("event:/triggernotice");
        robot = FMODUnity.RuntimeManager.CreateInstance("event:/NPC ticketmachine");
        Event.start();
    }
    public void StartGame(string LoadBaseSceneName)
    {
        Event.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        train.start();
        SceneManager.LoadScene(LoadBaseSceneName);

    }

    public void OpenControlsMenu(GameObject ControlPanel)
    {
        
        robot.start();
        ControlPanel.SetActive(true);
    }

    public void CloseControlsMenu(GameObject ControlPanel)
    {
        robot.start();
        ControlPanel.SetActive(false);
    }
}
