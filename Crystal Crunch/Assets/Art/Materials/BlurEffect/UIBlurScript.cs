using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlurScript : MonoBehaviour
{
    public Camera BlurCamera;
    public Material BlurMaterial;
    public RenderTexture RenTex;

    public List<Material> BlurMaterials;
    private void Start()
    {
        if(BlurCamera.targetTexture!=RenTex)
        {
            BlurCamera.targetTexture = RenTex;
        }
        //BlurCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        BlurMaterial.SetTexture("_RenderTex", BlurCamera.targetTexture);
        foreach(Material mat in BlurMaterials)
        {
            mat.SetTexture("_RenderTex", BlurCamera.targetTexture);
        }
    }
}
