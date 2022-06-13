using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

using Cinemachine;
public class PauseMenuManager : MonoBehaviour
{
    
    [SerializeField] GameObject PauseMenu;
    [SerializeField] TMP_Text QuestName;
    [SerializeField] TMP_Text ObjectiveText;
    [SerializeField] GameObject DefaultSelectedButton;

    [SerializeField] GameObject HelpMenu, KeyboardControls, XboxControls;


    public int ShowingQuestIndex = 0;

    [SerializeField] List<Quest> DisplayableQuests = new List<Quest>();

    [SerializeField] CinemachineVirtualCamera DefaultCam;
    int DefaultPriority, PausedPriority = 140;
    
    void Start()
    {
        DefaultPriority = DefaultCam.m_Priority;
        InputManager.inputManager.PInput.PlayerInputControls.PauseGame.performed += ctx
        =>
        {
            GamePauseFunction(InputManager.GameIsPaused);
        };

        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.PauseGame.performed += ctx
        =>
        {
            GamePauseFunction(InputManager.GameIsPaused);
        };

        InputManager.inputManager.PInput.PlayerInputControls.MapShortcut.performed += ctx
        =>
        {
            GamePauseFunction(InputManager.GameIsPaused);
        };

        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.MapShortcut.performed += ctx
        =>
        {
            GamePauseFunction(InputManager.GameIsPaused);
        };

        foreach (Quest q in QuestManager.questManager.GameQuests)
        {
            if (q.Unlocked == true && q.Completed == false)
            {
                DisplayableQuests.Add(q);
            }
        }
        GameEventsSystem.gameEventsSystem.UIEvents.OnSetStaticSetStaticQuestObjectiveQuestEvent(DisplayableQuests[ShowingQuestIndex]);

        GameEventsSystem.gameEventsSystem.QuestCompletedEvent += SetUI;
    }

    void SetUI()
    {
        DisplayableQuests.Clear();
        foreach(Quest q in QuestManager.questManager.GameQuests)
        {
            if(q.Unlocked == true && q.Completed == false)
            {
                DisplayableQuests.Add(q);
            }
        }
        
        if(DisplayableQuests.Count>0)
        {
            if(ShowingQuestIndex >= DisplayableQuests.Count)
            {
                ShowingQuestIndex = 0;
            }
            QuestName.text = DisplayableQuests[ShowingQuestIndex].QuestID;
            if(DisplayableQuests[ShowingQuestIndex].ObjectivesIndex >= DisplayableQuests[ShowingQuestIndex].MyObjectives.Count)
            {
                ObjectiveText.text = "No New Objective";
                return;
            }
            GameEventsSystem.gameEventsSystem.UIEvents.OnSetStaticSetStaticQuestObjectiveQuestEvent(DisplayableQuests[ShowingQuestIndex]);
            ObjectiveText.text = DisplayableQuests[ShowingQuestIndex].MyObjectives[DisplayableQuests[ShowingQuestIndex].ObjectivesIndex].ObjectiveDescription;
        }
        else
        {
            QuestName.text = "No Track Playing";
            ObjectiveText.text = "Find some new tunes!";
        }
    }

    public void ChangeShowingIndex(int numbertoadd)
    {
        if(Mathf.Abs(numbertoadd)>1 || Mathf.Abs(numbertoadd) < 1) //Making sure it's only dealing with 1's
        {
            numbertoadd = numbertoadd / Mathf.Abs(numbertoadd);
        }

        if(numbertoadd >0) //if positive 1
        {
            if(numbertoadd+ShowingQuestIndex == DisplayableQuests.Count)
            {
                ShowingQuestIndex = 0;
            }
            else
            {
                ShowingQuestIndex += numbertoadd;
            }
        }

        if(numbertoadd <0)  //if negative 1
        {
            if (numbertoadd + ShowingQuestIndex < 0)
            {
                ShowingQuestIndex = DisplayableQuests.Count-1;
            }
            else
            {
                ShowingQuestIndex += numbertoadd;
            }
        }

        SetUI();

    }

    public void GamePauseFunction(bool IsPaused)
    {
        DisplayableQuests.Clear();
        SetUI();
        GameEventsSystem.gameEventsSystem.UIEvents.OnPressPauseEvent(InputManager.GameIsPaused);
        if (InputManager.PlayerInDialogue == true)
        {
            return;
        }
        if(IsPaused == true)
        {
            PauseMenu.SetActive(false);
            DefaultCam.m_Priority = DefaultPriority;
            InputManager.GameIsPaused = false;
            EventSystem.current.SetSelectedGameObject(null);
            
            Time.timeScale = 1f;
        }
        else
        {
            PauseMenu.SetActive(true);
            DefaultCam.m_Priority = PausedPriority;
            InputManager.GameIsPaused = true;
            EventSystem.current.SetSelectedGameObject(DefaultSelectedButton);
            Time.timeScale = 0f;
        }
    }

    public void SwapControlsView()
    {
        if(KeyboardControls.activeInHierarchy)
        {
            XboxControls.SetActive(true);
            KeyboardControls.SetActive(false);
        }
        else
        {
            XboxControls.SetActive(false);
            KeyboardControls.SetActive(true);
        }
    }

    public void OpenObject(GameObject GO)
    {
        if(GO!=null)
        {
            GO.SetActive(true);
        }
    }

    public void CloseObject(GameObject GO)
    {
        if (GO != null)
        {
            GO.SetActive(false);
        }
    }

    public void SetFocus(GameObject GO)
    {
        if (GO != null)
        {
            EventSystem.current.SetSelectedGameObject(GO);
        }
    }
}
