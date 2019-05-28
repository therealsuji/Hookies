using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBoxCharacterController : MonoBehaviour
{

    new Camera camera;
    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;

    public float jumpforce;
    public float speed;
    public float gravityScale;

    void Start()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
        
        camera = GameObject.FindObjectOfType<Camera>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        // var characterRotation = camera.transform.rotation;
        // characterRotation.x = 0;
        // characterRotation.z = 0;

        // transform.rotation = characterRotation;
        
        HandlePlayerMovements();

    }

    private void HandlePlayerMovements()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, moveDirection.y, Input.GetAxis("Vertical") * speed);
    
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpforce;
            }
        }
        

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
