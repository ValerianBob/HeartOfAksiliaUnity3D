using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private СrystalsController сrystalsController;
    private Rigidbody enemyRigidBody;
    private PlayerController playerController;
    private BoxCollider attackTrigger;
    private GameObject build;

    private BuildHealth currentBuildAttaking;
    private BuildHealth mainBaseHealth;

    private GameObject mainBase;

    public int hitPoints;
    public float speed;
    public int damage;

    private float buildDistance;
    private float playerDistance;

    private Vector3 playerDirection;
    private Vector3 buildDirection;
    private Vector3 buildsPosition;
    private Vector3 mainBaseDirection;

    private bool enemyTargetChoice = false;

    private bool isCollidingPlayer = false;
    private bool isCollidingBuild = false;

    private void Start()
    {
        сrystalsController = GameObject.Find("MainBase").GetComponent<СrystalsController>();
        playerController = GameObject.Find("Kaylo").GetComponent<PlayerController>();
        attackTrigger = GetComponent<BoxCollider>();
        enemyRigidBody = GetComponent<Rigidbody>();

        mainBaseHealth = GameObject.Find("MainBase").GetComponent<BuildHealth>();
        mainBase = GameObject.Find("MainBase");
    }

    private void Update()
    {
        if (build == null)
        {
            enemyTargetChoice = false;
        }

        if (currentBuildAttaking == null)
        {
            isCollidingBuild = false;
        }

        playerDistance = Vector3.Distance(transform.position, playerController.transform.position);
        playerDirection = (playerController.transform.position - transform.position).normalized;

        mainBaseDirection = (mainBase.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (mainBaseHealth.currentHealth > 0)
        {
            enemyMovement(enemyTargetChoice);
        }
    }

    private void enemyMovement(bool enemyChoice)
    {
        if (playerController.isDead)
        {
            if (!enemyChoice)
            {
                enemyRigidBody.AddForce(mainBaseDirection * speed);
                Debug.Log(enemyChoice);
            }
            else
            {
                enemyRigidBody.AddForce(buildDirection * speed);
            }
        }
        else if (enemyChoice)
        {
            enemyRigidBody.AddForce(buildDirection * speed);
        }
        else
        {
            enemyRigidBody.AddForce(playerDirection * speed);
        }
    }

    private void EnemyHited(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Bullet"))
        {
            hitPoints--;
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
                сrystalsController.crystals += 1;
            }
            Destroy(otherCollider.gameObject);
        }
    }

    private void EnemyChoseAttackBuild(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Build"))
        {
            build = otherCollider.gameObject;
            buildDistance = Vector3.Distance(transform.position, otherCollider.transform.position);

            if (buildDistance < playerDistance)
            {
                enemyTargetChoice = true;

                buildsPosition = otherCollider.gameObject.transform.position;
                buildDirection = (buildsPosition - transform.position).normalized;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        EnemyChoseAttackBuild(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHited(other);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isCollidingPlayer)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine("DamagePlayer");
            }
        }
        
        if (!isCollidingBuild)
        {
            if (collision.gameObject.CompareTag("Build"))
            {
                currentBuildAttaking = collision.gameObject.GetComponent<BuildHealth>();
                StartCoroutine("DamageBuild");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine("DamagePlayer");
            isCollidingPlayer = false;
        }

        if (collision.gameObject.CompareTag("Build"))
        {
            StopCoroutine("DamageBuild");
            isCollidingBuild = false;
        }
    }

    private IEnumerator DamagePlayer()
    {
        while (true)
        {
            isCollidingPlayer = true;

            playerController.hitPoints -= damage;
            yield return new WaitForSeconds(0.5f);

            isCollidingPlayer = false;
        }
    }
    private IEnumerator DamageBuild()
    {
        while (true)
        {
            isCollidingBuild = true;

            currentBuildAttaking.currentHealth -= damage;
            yield return new WaitForSeconds(0.5f);
            isCollidingBuild = false;
        }
    }
}
