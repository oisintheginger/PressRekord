using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

[System.Serializable]
public enum TrackerType
{
    zero = 0,
    FullTracker = 1,
    XTracker = 2,
    YTracker = 3,
    Fixed = 4,
    FullTrackFixedComposite = 5
}

[System.Serializable]
public struct areaCameraProperties
{
    public Vector3 positionCenter; 
    public float CameraSize;
    public float TransitionSpeed;
    public TrackerType myType;
    public bool isTransitioning;
    [Tooltip("Positive value for weighting towards Player")][Range(-0.3f, 0.3f)] public float CompositeWeight; 
}

[RequireComponent(typeof(Collider2D))]
public class CameraTriggerAreaScript : MonoBehaviour
{
    public VolumeProfile VP;
    public bool changesPP = false;
    [SerializeField] int Identity;
    [SerializeField] areaCameraProperties myProps;

    public Transform CameraCenterPos;
    private void Awake()
    {
        this.gameObject.GetComponent<Collider2D>().isTrigger = true;
        myProps.positionCenter = this.transform.position;
        //if(CameraCenterPos !=null&& myProps.myType == TrackerType.Fixed)
        //{
            myProps.positionCenter = CameraCenterPos.position;
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameEventsSystem.gameEventsSystem.CameraEventsSystem.ChangeCamera(myProps, true);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Positive);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameEventsSystem.gameEventsSystem.CameraEventsSystem.RestoreDefaultCamera();
        }
        if (VP != null && changesPP == true)
        {

        }
    }
}
