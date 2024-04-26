using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCylinder : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Quaternion newQ = Quaternion.Euler(0, 0, 30f);

            transform.rotation *= newQ;
            //transform.Rotate(30, 30, 30);
        }

        /* Vector3 ditection = target.position - transform.position;

        Quaternion newRotation = Quaternion.LookRotation(ditection);

        transform.rotation = newRotation;*/
    }

}
