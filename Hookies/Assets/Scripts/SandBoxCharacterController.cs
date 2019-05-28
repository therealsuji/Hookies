using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBoxCharacterController : MonoBehaviour
{

    new Camera camera;
    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;

    public float speed;
    public float gravityScale;
    float jumpForce = 15;
    float verticalVelocity;
    private float gravityJump = 14.0f;
    public float gravity = -9.8f;
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
        var characterRotation = camera.transform.rotation;
        characterRotation.x = 0;
        characterRotation.z = 0;

        transform.rotation = characterRotation;
        
        HandlePlayerMovements();

    }

    private void HandlePlayerMovements()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        controller.Move(movement);


        if (controller.isGrounded)
        {
            verticalVelocity = -gravityJump * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravityJump * Time.deltaTime;

        }

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(jumpVector * Time.deltaTime);
    }
}
