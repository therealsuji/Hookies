using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour

{
  

    //public float speedH = 2.0f;
    //public float speedV = 2.0f;

    //private float yaw = 0.0f;
    //private float pitch = 0.0f;

    [SerializeField]
    public Transform target;
    private Vector3 offset;

    [SerializeField]
    private float rotateSpeed = 10f;

    [SerializeField]
    private Transform pivot;

    void Start()
    {
        offset = target.position - transform.position;

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;
    }

    void Update()
    {

        //Get the X position of the mouse and rotate the player
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        //Move the camera based on the current rotation of the player
        float yAngle = target.eulerAngles.y;
        float xAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(xAngle, yAngle, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;

        transform.LookAt(target);

        //yaw += speedH * Input.GetAxis("Mouse X");
        //pitch -= speedV * Input.GetAxis("Mouse Y");

        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

}
