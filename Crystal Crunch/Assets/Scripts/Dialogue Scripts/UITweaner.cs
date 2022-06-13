using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AnimationTween
{
    ScaleIn,
    PopUp,
    SlideIn
}

public enum CloseType
{
    Disable,
    ScaleDarken
}
public class UITweaner : MonoBehaviour
{
    [SerializeField] AnimationTween myAnimation;
    public AnimationCurve myCurve, closeCurve;
    public CloseType myCloseType;
    public float transitionSpeed = 0.5f;
    public Vector3 NotFocusScale = Vector3.one/2;

    public Vector3 FocusScale = Vector3.one;

    public Vector3 FocusColorStart = Vector3.one/2, FocusColorEnd = Vector3.one;
    public void OnOpen()
    {
        switch (myAnimation)
        {
            case AnimationTween.ScaleIn:
                    LeanTween.scale(this.gameObject, Vector3.one, transitionSpeed).setEase(myCurve);
                break;
        }
    }
    public void OnClose()
    {
        switch(myCloseType)
        {
            case CloseType.Disable:
                switch (myAnimation)
                {
                    case AnimationTween.ScaleIn:
                        LeanTween.scale(this.gameObject, Vector3.zero, transitionSpeed).setEase(closeCurve);
                        break;
                }
                break;
            
        }
        
    }
    public void Shrink()
    {
        LeanTween.scale(this.gameObject, NotFocusScale, transitionSpeed).setEase(myCurve);
        //ImageFadeColour(this.gameObject,1f,0.5f,1f,0.5f,1f,0.5f);
        ImageFadeColour(this.gameObject, FocusColorEnd.x, FocusColorStart.x, FocusColorEnd.y, FocusColorStart.y, FocusColorEnd.z, FocusColorStart.z);
    }
   public void UnShrink()
    {
        LeanTween.scale(this.gameObject, FocusScale, transitionSpeed).setEase(myCurve);
        ImageFadeColour(this.gameObject, FocusColorStart.x, FocusColorEnd.x, FocusColorStart.y, FocusColorEnd.y, FocusColorStart.z, FocusColorEnd.z);
    }
    void ImageFadeColour(GameObject imageToFade, float rStart = 0.5f, float rEnd = 1f, float gStart = 0.5f, float gEnd = 1f, float bStart = 0.5f, float bEnd = 1f)
    {
        LeanTween.value(imageToFade, rStart, rEnd, transitionSpeed).setOnUpdate((float val) =>
        {
            Image sr = imageToFade.GetComponent<Image>();
            Color newColor = sr.color;
            newColor.r = val;
            sr.color = newColor;
        });
        LeanTween.value(imageToFade, gStart, gEnd, transitionSpeed).setOnUpdate((float val) =>
        {
            Image sr = imageToFade.GetComponent<Image>();
            Color newColor = sr.color;
            newColor.g = val;
            sr.color = newColor;
        });
        LeanTween.value(imageToFade, bStart, bEnd, transitionSpeed).setOnUpdate((float val) =>
        {
            Image sr = imageToFade.GetComponent<Image>();
            Color newColor = sr.color;
            newColor.b = val;
            sr.color = newColor;
        });
    }

}
