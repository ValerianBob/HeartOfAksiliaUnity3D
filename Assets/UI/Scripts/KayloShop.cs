using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KayloShop : MonoBehaviour
{
    private PlayerController playerController;
    private СrystalsController сrystalsController;
    public Button buttonHeal;

    private void Start()
    {
        playerController = GameObject.Find("Player").transform.GetChild(0).GetComponent<PlayerController>();
        сrystalsController = GameObject.Find("MainBase").GetComponent<СrystalsController>();
        buttonHeal.onClick.AddListener(HealForKaylo);
    }

    private void Update()
    {
        BuyBlock();
    }

    private void HealForKaylo()
    {
        if (playerController.hitPoints < 100 && сrystalsController.crystals > 100)
        {
            playerController.hitPoints += 10;

            if (playerController.hitPoints > 100)
            {
                playerController.hitPoints = 100;
            }

            сrystalsController.crystals -= 100;
        }
    }

    private void BuyBlock()
    {
        if (сrystalsController.crystals < 100)
        {
            buttonHeal.image.color = Color.red;
        }
        else
        {
            buttonHeal.image.color = Color.white;
        }
    }
}
