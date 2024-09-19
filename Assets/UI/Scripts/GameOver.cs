using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private СrystalsController сrystalsController;
    private InGameMenu inGameMenu;
    private BuildsShop buildsShop;

    private PlayerController playerController;

    private BuildHealth mainBaseHealth;

    public GameObject gameOverMenu;
    public Button playAgain;
    public Button goInMainMenu;

    public TextMeshProUGUI resultTime;
    private GameTimer timer;

    public bool gameOver = false;

    private void Start()
    {
        mainBaseHealth = GetComponent<BuildHealth>();
        timer = GetComponent<GameTimer>();
        сrystalsController = GetComponent<СrystalsController>();
        inGameMenu = GetComponent<InGameMenu>();
        buildsShop = GetComponent<BuildsShop>();

        playerController = GameObject.Find("Player").transform.GetChild(0).GetComponent<PlayerController>();

        playAgain.onClick.AddListener(PlayAgain);
        goInMainMenu.onClick.AddListener(GoInMainMenu);
    }

    private void Update()
    {
        if (mainBaseHealth.currentHealth < 0 && !gameOver)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameOverMenu.SetActive(true);
            gameOver = true;

            SetResultTime();

            // Stop All gameplay stuff
            сrystalsController.stopCollectCrystalls = true;
            timer.stopTimer = true;
            inGameMenu.enabled = false;
            buildsShop.enabled = false;
            playerController.enabled = false;
        }
    }

    private void SetResultTime()
    {
        resultTime.text = "Result time " + timer.minutes.ToString() + ":" + timer.seconds.ToString();
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void GoInMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
