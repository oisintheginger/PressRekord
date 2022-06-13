using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spookyboi : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("Droprandom");
    }

    IEnumerator Droprandom()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0, 15));
        anim.SetTrigger("drop");
        yield return null;
    }
}
