using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround2D : MonoBehaviour
{
    [Header("Setting")]
    public float speed;
    public Transform target;
    public bool reverse = false;
    public bool freezeRotation = false;

    void Update()
    {
        if (reverse == true)
        {
            transform.RotateAround(target.position, Vector3.forward, speed);

            if (freezeRotation)
                transform.rotation = Quaternion.identity;


        }
        else
        {
            transform.RotateAround(target.position, Vector3.back, speed);

            if (freezeRotation)
                transform.rotation = Quaternion.identity;
        }
    }
}
