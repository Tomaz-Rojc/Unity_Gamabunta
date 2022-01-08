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

    public float turnSmoothTIme = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;

    bool isMoving = false;

    public AnimationManagerUI anim;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // jump, double jump
        if (isGrounded)
        {
            doubleJumped = false;
            canDoubleJump = false;
            canDash = true;
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetAnimation_Jump();
            MySoundManager.PlaySound("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            dashTime = -dashCooldown;
        }
        if (!isGrounded && !doubleJumped)
        {
            canDoubleJump = true;
        }
        if (canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            MySoundManager.PlaySound("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canDoubleJump = false;
            doubleJumped = true;
            canDash = true;
            dashTime = -dashCooldown;
            anim.SetAnimation_Jump();
        }

        // reset x, z velocity
        if (!isDashing)
        {
            velocity.x = 0f;
            velocity.z = 0f;
        }

        // dash activation
        if (canDash && dashTime <= -dashCooldown && Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetAnimation_Hit03();
            MySoundManager.PlaySound("dash");
            velocity += (transform.right * ha + transform.forward * va) * dashSpeed;
            dashTime = dashFullTime;
            canDash = false;
            isDashing = true;
        }

        // handle running
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            stamina -= Time.deltaTime;
            if (stamina > 0.2f) speed = runSpeed;
        }
        else
        {
            speed = moveSpeed;
            if (stamina < 2) stamina += Time.deltaTime;
        }

        // check if AWSD is pressed
        if (
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)
            )
        {
            isMoving = true;
            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0.2f && isGrounded && !isDashing)
            {
                anim.SetAnimation_Run();
            }
            else if (isGrounded && !isDashing)
            {
                anim.SetAnimation_Walk();
            }

        }
        else
        {
            if(isGrounded && !isDashing) {
                anim.SetAnimation_Idle();
            }
            isMoving = false;
        }

        // move left, right, forward, backward
        if (!isDashing && isMoving)
        {
            // Vector3 move = transform.right * ha + transform.forward * va;
            Vector3 move = new Vector3(ha, 0f, va).normalized;
            // controller.Move(move * speed * Time.deltaTime);

            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTIme);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        dashTime -= Time.deltaTime;
        if (isDashing && dashTime <= 0)
        {
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
