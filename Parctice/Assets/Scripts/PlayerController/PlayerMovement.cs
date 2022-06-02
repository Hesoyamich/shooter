using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int money;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    //Арс
    public GameObject death_panel;
    public GameObject HP_UI;
    public GameObject Ammo_UI;
    
    //

    public int hp;
    int maxhp;
    bool isAlive;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        air
    }

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        maxhp = hp;
        isAlive = true;
    }

    void Update() 
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (isAlive) 
        {
            MyInput();
            SpeedControl();
            StateHandler();
        }

        //
        HP_UI.GetComponent<Slider>().value = hp;
        //
       

        if (hp <= 0) KillPlayer();


        if(grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

         //Арс
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        //
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        if(Input.GetButton("Jump") && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler()
    {

        if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        else 
        {
            state = MovementState.air;
        } 
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope() && !exitingSlope) 
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if(rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        if (OnSlope())
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }

        else 
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if(flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            } 
        }
        
    }

    private void Jump() 
    {
        exitingSlope = true;
        // Сбрасываем вертикальную скорость
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * .5f + .3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection() 
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
    }

    public void HealPlayer(int heal)
    {
        hp += heal;
        if (hp > maxhp) hp = maxhp;
    }

    void KillPlayer()
    {
        isAlive = false;
        death_panel.SetActive(true);
    }

    public int GetMaxHp()
    {
        return maxhp;
    }
}
