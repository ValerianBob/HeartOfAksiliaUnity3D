using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildHealth : MonoBehaviour
{
    public Slider healthBar;
    private GameObject parentObject;
    private BuildsShop buildsShop;

    public int currentHealth;
    public int maxHealth;

    private void Start()
    {
        buildsShop = GameObject.Find("MainBase").GetComponent<BuildsShop>();

        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);

        parentObject = transform.parent.gameObject;
    }

    private void Update()
    {
        UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth < 0 && gameObject.name != "MainBase")
        {
            Destroy(parentObject);
        }
    }

    public void UpdateHealthBar(float currentHP, float maxHP)
    {
        healthBar.value = currentHP / maxHP;
    }

    private void OnMouseDown()
    {
        if (buildsShop.repairMode)
        {
            currentHealth += 20;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            buildsShop.repairMode = false;
        }
        else if (buildsShop.destroyMode && gameObject.name != "MainBase")
        {
            Destroy(parentObject);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            buildsShop.destroyMode = false;
        }
    }
}
