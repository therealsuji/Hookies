using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBoxCharacterController : MonoBehaviour
{

    new Camera camera;
    private Vector3 moveDirection = Vector3.zero;
    public float speed;
    CharacterController controller;
    void Start()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
        
        camera = GameObject.FindObjectOfType<Camera>();
        controller = GetComponent<CharacterController>();

    }

    void FixedUpdate()
    {

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;


        // var characterRotation = camera.transform.rotation;
        // characterRotation.x = 0;
        // characterRotation.z = 0;

        // transform.rotation = characterRotation;
        controller.Move(moveDirection * Time.deltaTime);

    }
}
