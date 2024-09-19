using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingsController : MonoBehaviour
{
    public Camera mainCamera;
    private GameObject tempBuild;
    private BoxCollider boxCollider;
    private BuildBlock buildBlock;
    private BuildsShop buildsShop;

    private int buildCount = 0;
    private float rotationSpeed = 50f;
    private bool buildingMode = false;

    private void Start()
    {
        buildsShop = GetComponent<BuildsShop>();
    }

    private void Update()
    {
        tempBuild = buildsShop.chosenBuild;

        // Got mouse control
        Vector3 mousePosition = Input.mousePosition;
        float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // Mouse builds on floor instead of on Canvas
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (buildsShop.generateBuildOneTime)
        {
            // Turn on mouse cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Building mode on and count builds
            buildingMode = true;
            buildCount += 1;

            tempBuild.name = buildsShop.buildName + buildCount;

            // Add build
            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(tempBuild, new Vector3(hit.point.x, buildsShop.alignmentY, hit.point.z), tempBuild.transform.rotation);
            }
            buildsShop.generateBuildOneTime = false;
        }

        if (buildsShop.buildingMode)
        {
            tempBuild = GameObject.Find(FindBuildByName());

            buildBlock = tempBuild.transform.GetChild(0).GetComponent<BuildBlock>();

            // Make build not collide with other object until builded
            boxCollider = tempBuild.transform.GetChild(0).GetComponent<BoxCollider>();
            boxCollider.isTrigger = true;

            // Make build follow mouse cursor
            if (Physics.Raycast(ray, out hit))
            {
                tempBuild.transform.position = new Vector3(hit.point.x, buildsShop.alignmentY, hit.point.z);
            }

            // Make build rotate by Mouse Wheel
            tempBuild.transform.Rotate(Vector3.up, mouseScrollWheel * rotationSpeed);
        }

        if (Input.GetMouseButtonDown(1) && buildsShop.buildingMode && buildBlock.block == false)
        {
            // Make build collide with other object
            boxCollider.isTrigger = false;

            // Turn off mouse cursor and building mode
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            buildsShop.buildingMode = false;
        }
    }

    private string FindBuildByName()
    {
        return buildsShop.buildName + buildCount + "(Clone)";
    }
}
