using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Transform rotationInfo;
    private PlayerController playerController;

    void Start()
    {
        rotationInfo = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.hitPoints < 0)
        {
            Death();
        }
        else
        {
            if (rotationInfo.transform.eulerAngles.y > 0 && rotationInfo.transform.eulerAngles.y < 90)
            {
                SetAnimationOfRotation("Run", "RunBackward", "RunLeft", "RunRight");
            }
            else if (rotationInfo.transform.eulerAngles.y > 90 && rotationInfo.transform.eulerAngles.y < 180)
            {
                SetAnimationOfRotation("RunLeft", "RunRight", "RunBackward", "Run");
            }
            else if (rotationInfo.transform.eulerAngles.y > 180 && rotationInfo.transform.eulerAngles.y < 270)
            {
                SetAnimationOfRotation("RunBackward", "Run", "RunRight", "RunLeft");
            }
            else if (rotationInfo.transform.eulerAngles.y > 270 && rotationInfo.transform.eulerAngles.y < 360)
            {
                SetAnimationOfRotation("RunRight", "RunLeft", "Run", "RunBackward");
            }
        }
    }

    private void Death()
    {
        animator.SetTrigger("Death");
    }

    private void SetAnimationOfRotation(string w, string s, string a, string d)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger(w);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetTrigger("idle");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger(s);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetTrigger("idle");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger(a);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetTrigger("idle");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger(d);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetTrigger("idle");
        }
    }
}