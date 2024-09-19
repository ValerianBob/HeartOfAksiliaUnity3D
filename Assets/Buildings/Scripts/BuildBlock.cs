using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBlock : MonoBehaviour
{
    public bool block = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Build") || other.CompareTag("Player"))
        {
            block = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Build") || other.CompareTag("Player"))
        {
            block = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        block = false;
    }
}
