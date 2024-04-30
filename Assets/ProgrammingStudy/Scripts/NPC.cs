using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform destination;
    //public int status = 0; // 0: Idle, 1: Walk, 2: Run
    public float speed = 3f;
    public float range = 0.5f;
    Animator animator;

    public enum Status
    {
        IDLE,
        WALK,
        PUSHBUTTON
    }
    public Status status = Status.IDLE;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        switch(status) // FSM
        {
            case Status.IDLE:
                animator.SetInteger("Status", (int)Status.IDLE);
                break;
            case Status.WALK:
                animator.SetInteger("Status", (int)Status.WALK);
                Move(destination);
                break;
            case Status.PUSHBUTTON:
                animator.SetInteger("Status", (int)Status.PUSHBUTTON);
                break;
        }
    }

    private void Move(Transform destination)
    {
        Vector3 direction = (destination.position - transform.position).normalized;
        float distance = (destination.position - transform.position).magnitude;

        if(distance < range)
        {
            // Button Click Animation 시작
            status = Status.PUSHBUTTON;
            print("클릭!");
        }

        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, transform.position.y, direction.z));
        transform.position += new Vector3(direction.x, transform.position.y, direction.z) * Time.deltaTime * speed;
    }

    void OnMoveButtonClickEvent()
    {
        status = Status.WALK;
    }
}
