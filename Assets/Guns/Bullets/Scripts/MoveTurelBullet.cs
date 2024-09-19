using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTurelBullet : MonoBehaviour
{
    private float bulletSpeed = 30.0f;
    private Vector3 bulletDirection;

    private TurelProperties tower;

    private void Start()
    {
        tower = GameObject.Find("Tower").GetComponent<TurelProperties>();

        bulletDirection = tower.directionToEnemy;
    }

    void Update()
    {
        transform.Translate(bulletDirection * bulletSpeed * Time.deltaTime);
        DeleteBullet();
    }

    public void DeleteBullet()
    {
        if (transform.position.z > 26 || transform.position.z < -26 || transform.position.y > 26 || transform.position.y < -26 || transform.position.x > 26 || transform.position.x < -26)
        {
            Destroy(gameObject);
        }
    }
}
