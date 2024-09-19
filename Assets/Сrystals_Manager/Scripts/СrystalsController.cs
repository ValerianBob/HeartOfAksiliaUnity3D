using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class СrystalsController : MonoBehaviour
{
    public int crystals = 0;
    public bool stopCollectCrystalls = false;

    void Start()
    {
        StartCoroutine(collectCrystalls());      
    }

    private void Update()
    {
        if (stopCollectCrystalls)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator collectCrystalls()
    {
        while (true)
        {
            crystals++;
            yield return new WaitForSeconds(1);
        }
    }
}

