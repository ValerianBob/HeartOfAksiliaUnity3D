using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrystalsUI : MonoBehaviour
{
    public СrystalsController сrystalsController;
    private TextMeshProUGUI crystals;

    private void Start()
    {
        crystals = GetComponent<TextMeshProUGUI>();      
    }
    private void Update()
    {
        crystals.text = "Crystals : " + сrystalsController.crystals.ToString();
    }
}
