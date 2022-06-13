using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum CommentType
{
    Positive = 0,
    Negative = 1,
    Boredom = 2
}

[System.Serializable]
public struct ViewerComment
{
    public string Comment;
    public Sprite EmojiType;
}


public class CombatCommentSection : MonoBehaviour
{
    [SerializeField] GameObject CommentGridParent;

    public List<string> CommentSentences = new List<string>();

    public List<Sprite> BoredomSprites = new List<Sprite>();
    public List<Sprite> PositiveSprites = new List<Sprite>();
    public List<Sprite> NegativeSprites = new List<Sprite>();

    public GameObject CommentObject;

    public Queue<GameObject> ViewableComments;
    public Queue<GameObject> CommentObjectPool;

    [SerializeField] int CommentObjectPoolCount = 10;
    [SerializeField] int MaximumViewableComments = 4;
    private void Start()
    {

        GameEventsSystem.gameEventsSystem.UIEvents.IncomingCommentEvent += IncomingComment;

        ViewableComments = new Queue<GameObject>();
        CommentObjectPool = new Queue<GameObject>();
        for(int i = 0; i< CommentObjectPoolCount; i++)
        {
            GameObject CommentInPool = Instantiate(CommentObject, this.transform);
            CommentInPool.transform.position = new Vector2(100000, 0);
            CommentInPool.name = "CommentInPool" + i.ToString();
            CommentObjectPool.Enqueue(CommentInPool);
        }


        GameEventsSystem.gameEventsSystem.DialEvents.StartDialogueEvent += Deactivate;
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent += Activate;
    }

    float LastCommentTime = 0f;
    [SerializeField]float BoredomTime = 30f;

    float CommentTimer= 5f, MaxTime = 5f;

    private void LateUpdate()
    {
        if(InputManager.PlayerInDialogue)
        {
            LastCommentTime = Time.time;
        }
        if(Time.time - LastCommentTime > BoredomTime)
        {
            if(CommentTimer<0)
            {
                IncomingComment(CommentType.Boredom);
                CommentTimer = MaxTime;
            }
            CommentTimer -= Time.deltaTime;
        }
    }

    bool processing = false;
    public void IncomingComment(CommentType CT)
    {
        StartCoroutine("ProcessComment", CT);
    }

    void Deactivate(DialogueObject n = null)
    {
        CommentGridParent.gameObject.transform.localScale = Vector3.zero;
    }

    void Activate()
    {
        CommentGridParent.gameObject.transform.localScale = new Vector3(1, -1, 1);
    }

    
    IEnumerator ProcessComment(CommentType CT)
    {
        while(InputManager.PlayerInDialogue)
        {
            yield return null;
        }
        while(processing)
        {
            yield return null;
        }
        processing = true;
        NewComment(CT);
        yield return new WaitForSeconds(.5f);
        processing = false;
        yield break;
    }


    void NewComment(CommentType IncomingComment)
    {
        Sprite EmoteToUse = null;
        string CommentToWrite = null;
        int index = 0;
        int stringIndex = 0;
        switch (IncomingComment)
        {
            case CommentType.Positive:
                index = Random.Range(0, PositiveSprites.Count);
                EmoteToUse = PositiveSprites[index];
                LastCommentTime = Time.time;
                CommentTimer = MaxTime;
                break;
            case CommentType.Negative:
                index = Random.Range(0, NegativeSprites.Count);
                EmoteToUse = NegativeSprites[index];
                LastCommentTime = Time.time;
                CommentTimer = MaxTime;
                break;
            case CommentType.Boredom:
                index = Random.Range(0, BoredomSprites.Count);
                EmoteToUse = BoredomSprites[index];
                break;
        }
        stringIndex = Random.Range(0, CommentSentences.Count);
        CommentToWrite = CommentSentences[stringIndex];
        GameObject NewCommentOBJ = CommentObjectPool.Dequeue();

        NewCommentOBJ.transform.SetParent(CommentGridParent.transform);
        CommentSectionObject comp = NewCommentOBJ.GetComponent<CommentSectionObject>();
        comp.EnterAnim();
        comp.Emote.sprite = EmoteToUse;
        comp.TextString.text = CommentToWrite;

        ViewableComments.Enqueue(NewCommentOBJ);

        if (ViewableComments.Count > MaximumViewableComments)
        {
            GameObject LastViewable = ViewableComments.Dequeue();
            CommentSectionObject c = LastViewable.GetComponent<CommentSectionObject>();
            LastViewable.transform.SetParent(this.transform.parent);
            LastViewable.transform.position = new Vector2(1000, 10000);
            c.Emote.sprite = c.DefaultEmpty;
            CommentObjectPool.Enqueue(LastViewable);
        }
    }

    [SerializeField] float timer = 5f;
    [SerializeField] bool bored = false;

    bool running = true;
    
    /*
    IEnumerator BoredomComment()
    {
        
        while(running)
        {
            Debug.Log("bacon bacon bacon");
            
            timer -= Time.deltaTime;
            if(timer<= 0)
            {
                bored = true;
            }
            int timemult = 1;
            if (bored)
            {
                Debug.Log("BORED");

                StartCoroutine("ProcessComment", CommentType.Negative);

                timemult += 1;
                yield return new WaitForSeconds(Mathf.Max(1f, 10f / timemult ));
            
            }
            yield return null;
        }
        
    }
    */
}
