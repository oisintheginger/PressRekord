using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class lighttimer : MonoBehaviour
{
    [SerializeField]List<Light2D> lightcontainor = new List<Light2D>();
    List<float> intensitycontainor = new List<float>();
    int counter;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentsInChildren(lightcontainor);
            StartCoroutine("timer");
    }


    IEnumerator timer()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        yield return new WaitForSecondsRealtime(Random.Range(1, 50));
        counter = 0;
        foreach(Light2D light in lightcontainor)
        {

            intensitycontainor.Add (light.intensity);
            light.intensity = 0;
            
        }
        yield return new WaitForSecondsRealtime(0.5f);
        foreach (Light2D light in lightcontainor)
        {
            light.intensity = intensitycontainor[counter];
            counter++;
            if (light.lightType == Light2D.LightType.Sprite)
            {
                yield return new WaitForSecondsRealtime(0.5f);
            }
            light.intensity = 0;
        }
        counter = 0;
        
        foreach (Light2D light in lightcontainor)
        {
            
            light.intensity= intensitycontainor[counter];
            counter++;
            
        }
        StartCoroutine("timer");
        yield return null;
    }
}
