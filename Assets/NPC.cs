using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform destination;
    public int status = 0; // 0: Idle, 1: Walk, 2: Run
    public float speed = 3f;
    public float range = 0.5f;
    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        switch(status)
        {
            case 0:
                animator.SetInteger("Status", 0);
                break;
            case 1:
                animator.SetInteger("Status", 1);
                Move(destination);
                break;
            case 2:
                animator.SetInteger("Status", 2);
                break;
        }
    }

    private void Move(Transform destination)
    {
        Vector3 direction = (destination.position - transform.position).normalized;
        float distance = (destination.position - transform.position).magnitude;

        if(distance < range)
        {
            print(distance);

            // Button Click Animation 시작
            status = 2;
            print("클릭!");
        }

        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, transform.position.y, direction.z));
        transform.position += new Vector3(direction.x, transform.position.y, direction.y) * Time.deltaTime * speed;
    }

    void OnMoveButtonClickEvent()
    {
        status = 1;
    }
}
