using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class SubscribeToEndDialogue : MonoBehaviour
{
    public GameObject FadeInTrigger;
    public  DialogueObject KatIntroDialogue;
    [SerializeField] GameObject Foreground;
    [SerializeField] GameObject Background;

    public UnityEvent InvokeOnEndDialogue;
    private void Awake()
    {
        FadeInTrigger.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    private void Start()
    {
        Invoke("StartDialogueDelay", .5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent += EndDialogue;
    }

    void EndDialogue()
    {
        InvokeOnEndDialogue.Invoke();
    }
    


    void StartDialogueDelay()
    {
        GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(KatIntroDialogue);
    }
}
