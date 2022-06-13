using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommentSectionObject : MonoBehaviour
{
    public TMP_Text TextString;
    public Image Emote;
    public Sprite DefaultEmpty;
    public Animator CommentAnimator;

    public const string EnterTrigger = "Enter";
    public const string ExitTrigger = "Exit";
    private void Awake()
    {
        CommentAnimator = this.GetComponent<Animator>();
    }

    public void EnterAnim()
    {
        CancelInvoke();
        CommentAnimator.SetBool("Displaying", true);
        CommentAnimator.SetTrigger(EnterTrigger);
        Invoke("InvokeExitAnim", 4f);
    }

    public void InvokeExitAnim()
    {
        CommentAnimator.SetBool("Displaying", false);
        CommentAnimator.SetTrigger(ExitTrigger);
    }
}
