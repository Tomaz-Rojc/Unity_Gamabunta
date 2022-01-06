using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speed;
    public float moveSpeed = 2f;
    public float runSpeed = 4f;
    public float stamina = 2f;
    public float jumpHeight = 3f;
    private bool canDoubleJump = false;
    private bool doubleJumped = false;
    public float dashSpeed = 40f;
    public float dashFullTime = 3f;
    public float dashCooldown = 0.5f;
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

    // Player UI
    public Slider dashSldier;
    public Slider staminaSlider;
        

    // Start is called before the first frame update
    void Start()
    {
        dashTime = dashFullTime;
        speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {   
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float ha = Input.GetAxis("Horizontal");
        float va = Input.GetAxis("Vertical");

        // reset fall speed
        if (isGrounded && velocity.y < 0) {
            velocity.y = 0f;
        }

        // jump, double jump
        if (isGrounded) {
            doubleJumped = false;
			canDoubleJump = false;
			canDash = true;
        }
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            MySoundManager.PlaySound("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            dashTime = -dashCooldown;
        }
        if (!isGrounded && !doubleJumped) {
			canDoubleJump = true;
		}
        if (canDoubleJump && Input.GetKeyDown(KeyCode.Space)) {
            MySoundManager.PlaySound("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canDoubleJump = false;
            doubleJumped = true;
            canDash = true;
            dashTime = -dashCooldown;
        }

        // reset x, z velocity
        if (!isDashing) {
            velocity.x = 0f;
            velocity.z = 0f;
        }

        // dash activation
        if (canDash && dashTime <= -dashCooldown && Input.GetKeyDown(KeyCode.LeftControl)) {
            MySoundManager.PlaySound("dash");
            velocity += (transform.right * ha + transform.forward * va) * dashSpeed;
            dashTime = dashFullTime;
            canDash = false;
            isDashing = true;
        }

        // handle running
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0) {
            stamina -= Time.deltaTime;
            if (stamina > 0.2f) speed = runSpeed;
        } else {
            speed = moveSpeed;
            if (stamina < 2) stamina += Time.deltaTime;
        }
    	// move left, right, forward, backward
        if (!isDashing) {
            Vector3 move = transform.right * ha + transform.forward * va;
            controller.Move(move * speed * Time.deltaTime);
        }

        dashTime -= Time.deltaTime;
        if (isDashing && dashTime <= 0) {
        // dash just ended
            isDashing = false;
        }

        // add gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Update slider values
        calculateSliders();
    }

    private void calculateSliders()
    {
        dashSldier.value = -dashTime * 2;
        staminaSlider.value = stamina / 2;
    }
}
