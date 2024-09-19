using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class BuildsShop : MonoBehaviour
{
    public List<GameObject> buildings;

    public Button destroyBuild;
    public Button buyWall;
    public Button buyTurel;
    public Button buyFarmHelper;
    public Button buyRepair;

    public GameObject openShop;
    public GameObject chosenBuild;
    private СrystalsController crystalsController;

    private InGameMenu inGameMenu;

    public bool isOpen = false;
    public string buildName = "";
    public bool buildingMode = false;
    public bool generateBuildOneTime = false;
    public float alignmentY;

    public bool repairMode = false;
    public bool destroyMode = false;

    private void Start()
    {
        crystalsController = GetComponent<СrystalsController>();

        inGameMenu = GetComponent<InGameMenu>();

        destroyBuild.onClick.AddListener(DestroyBuild);

        buyWall.onClick.AddListener(BuyWall);
        buyFarmHelper.onClick.AddListener(BuyFarmHelper);
        buyTurel.onClick.AddListener(BuyTurel);
        buyRepair.onClick.AddListener(BuyRepair);
    }

    private void Update()
    {
        if (inGameMenu.isPaused)
        {
            isOpen = false;
            openShop.SetActive(isOpen);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            openShop.SetActive(isOpen);

            Cursor.visible = isOpen;
            if (isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        // If misclicked on Destroy build, You can turn off destoy mode off
        if (Input.GetMouseButtonDown(0) && destroyMode)
        {
            destroyMode = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        BuyBlock();
    }

    private void BuyWall()
    {
        if (crystalsController.crystals >= 50)
        {
            buildName = "Wall";
            buildingMode = true;
            generateBuildOneTime = true;
            chosenBuild = buildings[0];
            alignmentY = 0.5f;

            isOpen = !isOpen;
            openShop.SetActive(isOpen);

            crystalsController.crystals -= 50;
        }
    }

    private void BuyFarmHelper()
    {
        if (crystalsController.crystals >= 200)
        {
            buildName = "FarmHelper";
            buildingMode = true;
            generateBuildOneTime = true;
            chosenBuild = buildings[1];
            alignmentY = 1f;

            isOpen = !isOpen;
            openShop.SetActive(isOpen);

            crystalsController.crystals -= 200;
        }
    }

    private void BuyTurel()
    {
        if (crystalsController.crystals >= 100)
        {
            buildName = "Turel";
            buildingMode = true;
            generateBuildOneTime = true;
            chosenBuild = buildings[2];
            alignmentY = 1.7f;

            isOpen = !isOpen;
            openShop.SetActive(isOpen);

            crystalsController.crystals -= 100;
        }
    }

    private void BuyRepair()
    {
        if (crystalsController.crystals >= 20)
        {
            repairMode = true;

            isOpen = !isOpen;
            openShop.SetActive(isOpen);

            crystalsController.crystals -= 20;
        }
    }

    private void DestroyBuild()
    {
        destroyMode = true;

        isOpen = !isOpen;
        openShop.SetActive(isOpen);
    }

    private void BuyBlock()
    {
        if (crystalsController.crystals < 50)
        {
            buyWall.image.color = Color.red;
        }
        else
        {
            buyWall.image.color = Color.white;
        }

        if (crystalsController.crystals < 200)
        {
            buyFarmHelper.image.color = Color.red;
        }
        else
        {
            buyFarmHelper.image.color = Color.white;
        }

        if (crystalsController.crystals < 20)
        {
            buyRepair.image.color = Color.red;
        }
        else
        {
            buyRepair.image.color = Color.white;
        }

        if (crystalsController.crystals < 100)
        {
            buyTurel.image.color = Color.red;
        }
        else
        {
            buyTurel.image.color = Color.white;
        }
    }
}
