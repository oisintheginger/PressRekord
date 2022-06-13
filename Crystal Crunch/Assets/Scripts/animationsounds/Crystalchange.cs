using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Crystalchange : MonoBehaviour
{
    Light2D emit;
    float intensityvalue;
    //[SerializeField]int iteration =20;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //emit = GetComponent<Light2D>();
        //intensityvalue = emit.intensity;
        //StartCoroutine("change");
        anim = GetComponent<Animator>();
        anim.Play("Base Layer.crystal pink single", 0, Random.Range(0.1f, 1));
    }

  /* IEnumerator change()
    {
       float range = Random.Range(intensityvalue +0.1f, intensityvalue + 0.2f);
        for (int i = 0; i < iteration; i++)
        {
            
            emit.intensity = emit.intensity + ((range - intensityvalue) / iteration);

            yield return new WaitForSecondsRealtime(0.5f / iteration);
        }
        yield return new WaitForSecondsRealtime(0.5f);

        for (int i = 0; i < iteration; i++)
        {
            
            emit.intensity = emit.intensity + ((intensityvalue - range) / iteration);

            yield return new WaitForSecondsRealtime(0.5f / iteration);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine("change");
        yield return null;
    }*/
}
