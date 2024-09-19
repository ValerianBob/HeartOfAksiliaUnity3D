using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;

    public TextMeshProUGUI wayCounterText;
    public GameTimer timer;
    private Coroutine cripWaysManager;
    private BuildHealth MainBaseHealth;

    private int wayCounter;
    private int minutesCounter = 1;
    private bool isCripAttack = false;
    private float spawnerSpeed = 1f;

    private int index3to6Way;
    private int index6to9Way;

    private Vector3[] spawnPositions =
    {
        new Vector3(-15, 1, 13),
        new Vector3(16, 1, 13),
        new Vector3(16, 1, -16),
        new Vector3(-20, 1, -16)
    };

    private void Start()
    {
        MainBaseHealth = GameObject.Find("MainBase").GetComponent<BuildHealth>();
    }

    private void Update()
    {
        index3to6Way = Random.Range(0, 2);
        index6to9Way = Random.Range(0, 3);
        if (MainBaseHealth.currentHealth > 0)
        {
            if (timer.seconds == 20 && isCripAttack == false)
            {
                isCripAttack = true;
                wayCounter += 1;
                StartCoroutine("SpawnEnemy");
                wayCounterText.text = wayCounter.ToString() + " Wave";
            }
            if (timer.minutes == minutesCounter)
            {
                isCripAttack = false;
                minutesCounter += 1;
                spawnerSpeed -= 0.1f;
                StopCoroutine("SpawnEnemy");
            }
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (wayCounter > 0 && wayCounter <= 3)
            {
                Instantiate(enemies[0], spawnPositions[Random.Range(0, spawnPositions.Length)], enemies[0].gameObject.transform.rotation);
            }
            else if (wayCounter > 3 && wayCounter <= 6)
            {
                Instantiate(enemies[index3to6Way], spawnPositions[Random.Range(0, spawnPositions.Length)], enemies[index3to6Way].gameObject.transform.rotation);
            }
            else if (wayCounter > 6 && wayCounter <= 9)
            {
                Instantiate(enemies[index6to9Way], spawnPositions[Random.Range(0, spawnPositions.Length)], enemies[index6to9Way].gameObject.transform.rotation);
            }
            
            yield return new WaitForSeconds(spawnerSpeed);
        }
    }
}
