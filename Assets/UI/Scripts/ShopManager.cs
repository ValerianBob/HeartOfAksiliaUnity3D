using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject KayloShop;
    public GameObject BuildsShop;
    public Button buttonBuildsShop;
    public Button buttonKayloShop;

    private void Start()
    {
        buttonBuildsShop.onClick.AddListener(ShowBuidsShop);
        buttonKayloShop.onClick.AddListener(ShowKayloShop);
    }

    private void ShowBuidsShop()
    {
        BuildsShop.SetActive(true);
        KayloShop.SetActive(false);
    }

    private void ShowKayloShop()
    {
        BuildsShop.SetActive(false);
        KayloShop.SetActive(true);
    }
}
