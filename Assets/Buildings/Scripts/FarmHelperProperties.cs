using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmHelperProperties : MonoBehaviour
{
    private СrystalsController crystalsController;

    private void Start()
    {
        crystalsController = GameObject.Find("MainBase").GetComponent<СrystalsController>();

        StartCoroutine(MakeMoreCrystals());
    }

    IEnumerator MakeMoreCrystals()
    {
        while (true)
        {
            crystalsController.crystals += 1;
            yield return new WaitForSeconds(1);
        }
    }
}
