using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurelProperties : MonoBehaviour
{
    private GameObject enemy;
    public GameObject bullet;
    private GameObject parentObject;
    private BuildsShop buildsShop;

    public Vector3 directionToEnemy;

    private bool targetLocked = false;

    private int count = 1;

    private void Start()
    {
        buildsShop = GameObject.Find("MainBase").GetComponent<BuildsShop>();
        
        parentObject = transform.parent.gameObject;
    }

    private void Update()
    {
        if (enemy != null)
        {
            directionToEnemy = (enemy.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        if (enemy != null && !targetLocked)
        {
            count += 1;
            Debug.Log(count);
            StartCoroutine("TurelFire");
            targetLocked = true;
        }
        else if (enemy == null)
        {
            StopCoroutine("TurelFire");
            targetLocked = false;
        }
    }

    private void OnTriggerStay(Collider otherTrigger)
    {
        if (otherTrigger.gameObject.CompareTag("Crip"))
        {
            enemy = otherTrigger.gameObject;
        }
    }

    IEnumerator TurelFire()
    {
        while (true)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), bullet.transform.rotation);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnMouseDown()
    {
        if (buildsShop.destroyMode)
        {
            Destroy(parentObject);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            buildsShop.destroyMode = false;
        }
    }
}
