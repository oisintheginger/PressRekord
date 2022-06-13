using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class SecondAreaPlayerLight : MonoBehaviour
{
    [SerializeField] GameObject PlayerLight;
    [SerializeField] Light2D PLight;

    [SerializeField] float MaxRadius = 2.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerStats._CurrentPlayerStats.gameObject)
        {
            if(PlayerLight.activeInHierarchy == true)
            {

                return;
            }
            StopCoroutine(GlowEnd());
            StartCoroutine(GlowStart());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerStats._CurrentPlayerStats.gameObject)
            PlayerLight.transform.position = PlayerStats._CurrentPlayerStats.transform.position + new Vector3(0, 1.1f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerStats._CurrentPlayerStats.gameObject)
        {
            if (PlayerLight.activeInHierarchy == false)
            {
                return;
            }
            StopCoroutine(GlowStart());
            StartCoroutine(GlowEnd());
        }
    }

    IEnumerator GlowStart()
    {
        PlayerLight.SetActive(true);
        PLight.pointLightOuterRadius = 0;
        while(PLight.pointLightOuterRadius < MaxRadius)
        {
            PLight.pointLightOuterRadius += Time.deltaTime * 5f;
            yield return null;
        }
        yield break;
    }

    IEnumerator GlowEnd()
    {
        PLight.pointLightOuterRadius = MaxRadius;
        while (PLight.pointLightOuterRadius > 0)
        {
            PlayerLight.transform.position = PlayerStats._CurrentPlayerStats.transform.position;
            PLight.pointLightOuterRadius -= Time.deltaTime * 5f;
            yield return null;
        }
        PlayerLight.SetActive(false);
        yield break;
    }
}
