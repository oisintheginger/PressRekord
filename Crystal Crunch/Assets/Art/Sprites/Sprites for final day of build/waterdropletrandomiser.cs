using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class waterdropletrandomiser : MonoBehaviour
{
    Animator anim;
    FMOD.Studio.EventInstance waterdroplet;
 

  
    
    // Start is called before the first frame update
    void Start()
    {
        waterdroplet = FMODUnity.RuntimeManager.CreateInstance("event:/water droplets sfx");
        waterdroplet.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        anim = GetComponent<Animator>();
        StartCoroutine("Droprandom");
    }
   

    IEnumerator Droprandom()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0, 15));
        anim.SetTrigger("drop");
        yield return null;
    }

  public void waterdropletsfx()
    {
        waterdroplet.start();

       
    }
}
