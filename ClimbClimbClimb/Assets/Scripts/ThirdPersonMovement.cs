using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform Camera;
    public Animator animator;
   
    private float verticalVelocity;
    public float speed = 22f;
    private float playerSpeedWFalling = 20;
    private float maxSpeed = 30f;
    public float currentSpeed;
   
  
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lastMoveDirection = Vector3.zero;
    private Vector3 impact = Vector3.zero;
  
    public float fallSpeed = 90; //adjusting downward movement
    public float jumpForce = 20f; //how powerful jumping is
    public float rotationSpeed = 5f; //
    public float wallJumpForce = 10f; //how powerful jumping off walls is
    public float gravity = 9.81f; //how strong gravity is
    public float turnSmoothTime = 0.1f; //smoothing on camera rotation

    public float knockbackForce = 10f; //strength of getting hit by enemy obstacles
    public float knockbackDuration = 5f; //how long knockback lasts



    private bool isGrounded; //player is on the ground
    private bool isMoving;
    private bool isJumping;
    private bool isFalling;
    private bool isWallJumping;
    private bool canWallJump; //player can wall jump or not
    private bool isHit; //player was hit or not


    private float turnSmoothVelocity; //
    private Vector3 velocity; //current velocity
    private Vector3 lastWallNormal; //normal vector of the last wall touched

    


    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;    
        animator = GetComponent<Animator>();
        playerSpeedWFalling = speed / 2;
    }

    void Update()
    {
        
        /*
            Animation setting
        */
        isGrounded = controller.isGrounded;

        if (controller.isGrounded) 
        {

            //Debug.Log("Grounded: "+controller.isGrounded);

            animator.SetBool("isGrounded", isGrounded);
            animator.SetBool("isFalling", false);
            
            isFalling = false;
            
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isGrounded", false);
                verticalVelocity = jumpForce;
            }

            lastWallNormal = Vector3.zero;    
        }
        else 
        { 
            verticalVelocity -= gravity * Time.deltaTime;
            moveDirection = lastMoveDirection;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
            animator.SetBool("isMoving", false);
        }
       

        /*
            Horizontal movement based on camera
        */
        Vector3 cameraForward = Camera.forward;
        Vector3 cameraRight = Camera.right; 
        moveDirection.y = 0; 
        moveDirection.Normalize();

        //for efficiency's sake, should check if these are zero or not before doing other things
        moveDirection.x = Input.GetAxis("Horizontal") * speed;
        moveDirection.z = Input.GetAxis("Vertical") * speed;

        moveDirection = (cameraForward * moveDirection.z + cameraRight * moveDirection.x).normalized * speed;

        if (moveDirection.magnitude >= 0.1f && !isHit) 
        {

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
       
            animator.SetBool("isMoving", true);
          
        }
        else 
        {
            animator.SetBool("isMoving", false);
        }
        
        /*
            Vertical movement based on custom gravity
        */
        moveDirection.y = verticalVelocity * 4f;
        controller.Move(moveDirection * Time.deltaTime);
       
    }

    /*
        All Collisions handler
    */
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        /*
            Wall Jumping
        */
        if (!isGrounded && hit.normal.y < 0.1f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(hit.normal != lastWallNormal && canWallJump) 
                { 
                    Debug.DrawRay(hit.point, hit.normal, Color.white, 1.25f);
                    verticalVelocity = jumpForce * 1.5f;
                    moveDirection = hit.normal * 12;
                    lastWallNormal = hit.normal;  
                }
            }
        }
        if (hit.gameObject.CompareTag("noWallJump"))
        {
            canWallJump = false;
            //Debug.Log("cant wall jump sorri!");
        }
        else 
        {
            canWallJump = true;    
        }

        /*
            Getting hit
        */
        if (hit.gameObject.CompareTag("enemy"))
        {
            animator.SetBool("isHit", true);
            Vector3 enemyPos = hit.transform.position;
            OnHitByEnemy(enemyPos);           
            Debug.Log("HIT!");
        }

    }
    public void OnHitByEnemy(Vector3 enemyPostion) 
    {
        Vector3 knockbackDir = (transform.position - enemyPostion).normalized;
        knockbackDir.y = 0.5f;
        StartCoroutine(Knockback(knockbackDir));
    }
    private IEnumerator Knockback(Vector3 direction) 
    {
        float timer = 0;
     
        while (timer < knockbackDuration) 
        {
            isHit = true;
          
            controller.Move(direction * knockbackForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        isHit = false;
        animator.SetBool("isHit", false);
    }
    
}

