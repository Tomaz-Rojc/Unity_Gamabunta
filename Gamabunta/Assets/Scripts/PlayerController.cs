using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float jumpHeight = 3f;
    private bool canDoubleJump = false;
    private bool doubleJumped = false;
    public float dashSpeed = 40f;
    public float dashFullTime = 3f;
    public float dashTime;
    private bool canDash = true;
    private bool isDashing = false;
    public float gravity = -9.81f;
    Vector3 velocity;
    public float groundDistance = 1f;
    public LayerMask groundMask;
    bool isGrounded;

    public CharacterController controller;
    public Transform groundCheck;
        

    // Start is called before the first frame update
    void Start()
    {
        dashTime = dashFullTime;
    }

    // Update is called once per frame
    void Update()
    {   
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        // reset gravity
        if (isGrounded && velocity.y < 0) {
            Debug.Log("Grounded");
            velocity.y = 0f;
        }

        // jump, double jump
        if (isGrounded) {
            doubleJumped = false;
			canDoubleJump = false;
			canDash = true;
        }
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        float ha = Input.GetAxis("Horizontal");
        float va = Input.GetAxis("Vertical");

        Vector3 move = transform.right * ha + transform.forward * va;
        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
