using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public bool isPaused = false;
    public bool isRunningOptions = false;

    public GameObject pauseGameMenu;
    public GameObject MenuNavigation;
    public GameObject Options;
    public GameObject Back;

    private BuildsShop buildsShop;

    private void Start()
    {
        buildsShop = GetComponent<BuildsShop>();
    }

    void Update()
    {
        BlockOrUnblockBuildsShopMenu();
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            TogglePause();
        }
    }

    private void BlockOrUnblockBuildsShopMenu()
    {
        if (isPaused)
        {
            buildsShop.enabled = false;
        }
        else
        {
            buildsShop.enabled = true;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (!isPaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        Time.timeScale = isPaused ? 0 : 1; 
        pauseGameMenu.SetActive(isPaused);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void LoadOptions()
    {
        isRunningOptions = !isRunningOptions;
        Options.SetActive(isRunningOptions);
        MenuNavigation.SetActive(!isRunningOptions);
    }

    public void LoadBack()
    {
        Options.SetActive(false);
        isRunningOptions = !isRunningOptions;
        MenuNavigation.SetActive(!isRunningOptions);
    }
}
