using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float runSpeed = 9f;
    [SerializeField] private float jumpSpeed = 1f;
    [SerializeField] private float gravitySpeed = 5f;
    private CharacterController characterController;
    
    float horizontalInput = 0f;
    float verticalInput = 0f;
    float horizontalVelocity;
    float verticalVelocity;

    bool characterGrounded;
    bool jumpCheck;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(characterController.isGrounded)
        {
            characterGrounded = true;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetAxisRaw("Jump") == 1)
        {
            jumpCheck = true;
        }


    }

    void FixedUpdate()
    {
        Debug.Log("jumpCheck: " + jumpCheck + " , " + Input.GetAxisRaw("Jump"));
        horizontalVelocity = horizontalInput * runSpeed * Time.deltaTime;

        if (characterGrounded == true)
        {
            characterGrounded = false;
            verticalVelocity = -gravitySpeed * Time.deltaTime;
            if (jumpCheck == true)
            {
                verticalVelocity = jumpSpeed;
            }
        }
        else
        {
            // caps gravity, obv change later
            if(verticalVelocity < -0.3f)
            {
                verticalVelocity -= 0;
            }
            else
            {
                verticalVelocity -= gravitySpeed * Time.deltaTime;
            }
        }
        jumpCheck = false;

        Debug.Log(verticalVelocity + "after check");
        Vector3 characterMovement = new Vector3(horizontalVelocity, verticalVelocity, 0);

        characterController.Move(characterMovement);


    }
}
