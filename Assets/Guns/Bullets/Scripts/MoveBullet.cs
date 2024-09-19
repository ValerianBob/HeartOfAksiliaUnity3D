using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private float bulletSpeed = 30.0f;
    public GameObject player;
    public Vector3 bullet_direction;

    void Start()
    {
        player = GameObject.Find("Kaylo");
        bullet_direction = player.transform.forward + new Vector3(-0.2f, 0, 0);
    }

    void Update()
    {
        transform.Translate(bullet_direction * bulletSpeed * Time.deltaTime);
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
