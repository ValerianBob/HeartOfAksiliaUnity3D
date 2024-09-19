using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Gun;
    public GameObject bullet;
    public Button buyBackButton;
    public GameObject respawnUI;
    public TextMeshProUGUI textRespawn;
    public InGameMenu IGM;
    
    private Rigidbody rb;

    private Animator animator;

    public SoundController soundController;
    public СrystalsController crystalsController;

    public int hitPoints = 100;
    private int respawnSeconds = 5;
    private int crystallsForBuyBack = 30;
    private float speed = 50f;
    private float rotationSpeed = 8f;

    private float fireRate = 10.0f;
    private float nextFireTime = 0.0f;

    public bool isDead = false;
    public bool startRespawn = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        buyBackButton.onClick.AddListener(BuyBack);
    }

    void Update()
    {
        PlayerDead();

        if (!IGM.isPaused && !isDead)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            MovementPlayer();
            RotatePlayer();
        }
    }

    // Move player on WASD
    private void MovementPlayer()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        rb.AddForce(Vector3.forward * inputZ * speed);
        rb.AddForce(Vector3.right * inputX * speed);
    }

    // Rotate player on Mouse 
    private void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up, mouseX * rotationSpeed);
    }

    private void Fire()
    {
        Instantiate(bullet, new Vector3(Gun.transform.position.x, Gun.transform.position.y, Gun.transform.position.z), bullet.transform.rotation);
        soundController.Shot();
    }

    private void PlayerDead()
    {
        if (hitPoints < 0)
        {
            isDead = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ShowTimeToRespawn();
        }
    }

    private void ShowTimeToRespawn()
    {
        if (isDead == true && !startRespawn)
        {
            startRespawn = true;
            respawnUI.SetActive(true);
            buyBackButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = $"Buy Back ({crystallsForBuyBack})";
            StartCoroutine("RespawnTime");
        }
    }

    IEnumerator RespawnTime()
    {
        int sec = respawnSeconds;

        while(true)
        {

            textRespawn.text = $"{sec} seconds";
            --sec;

            yield return new WaitForSeconds(1f);

            Debug.Log(sec);

            if (sec == 0)
            {
                RespawnKaylo();
                yield break;
            }
        }
    }

    private void RespawnKaylo()
    {
        respawnUI.SetActive(false);
        isDead = false;
        startRespawn = false;
        respawnSeconds += 5;
        crystallsForBuyBack += 30;
        hitPoints = 100;
        animator.SetTrigger("idle");
        animator.ResetTrigger("Death");
    }

    private void BuyBack()
    {
        if (!IGM.isPaused)
        {
            Debug.Log("Button");
            if (crystalsController.crystals >= crystallsForBuyBack)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                crystalsController.crystals -= crystallsForBuyBack;
                StopCoroutine("RespawnTime");
                RespawnKaylo();
            }
        }
    }
}
