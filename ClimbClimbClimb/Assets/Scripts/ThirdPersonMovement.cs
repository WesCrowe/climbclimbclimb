using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
   
    public AudioSource src; //audiosource component of the player
    public AudioClip jumpSound, runSound, hurtSound; //sound files

    public CharacterController controller;
    public Transform Camera; //main camera
    public Animator animator;
   
    public float runSoundCoolDown; //time between steps

    private float verticalVelocity;
    public float speed = 22f;
    private float playerSpeedWFalling = 20;
    private float maxSpeed = 30f;
    public float currentSpeed;
   
  
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lastMoveDirection = Vector3.zero;
    private Vector3 impact = Vector3.zero;
  
    public float fallSpeed = 90; 
    public float jumpForce = 20f;
    public float rotationSpeed = 5f;
    public float wallJumpForce = 10f;
    public float gravity = 9.81f;
    public float turnSmoothTime = 0.1f;

    public float knockbackForce = 10f;
    public float knockbackDuration = 5f;

    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    private bool isGrounded;
    private bool isMoving;
    private bool isJumping;
    private bool isFalling;
    private bool isWallJumping;
    private bool canWallJump;
    private bool isHit; 


    private float turnSmoothVelocity;
    private Vector3 velocity;
    private Vector3 lastWallNormal;

    public GameObject winMenuUI;
    public Timer timer;




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

        //attempt to smooth jumping responsiveness
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            jumpBufferCounter = jumpBufferTime;
        }
     
        jumpBufferCounter -= Time.deltaTime;
        
        if (controller.isGrounded) 
        {
            animator.SetBool("isGrounded", isGrounded);
            animator.SetBool("isFalling", false);
            
            isFalling = false;
            
            verticalVelocity = -gravity * Time.deltaTime;
           
            if (jumpBufferCounter > 0) 
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
                animator.SetBool("isGrounded", false);
                verticalVelocity = jumpForce;
                
                //play jumping sound
                src.clip = jumpSound;
                src.Play();
            }

            lastWallNormal = Vector3.zero;    
        }
        else 
        { 
            verticalVelocity -= gravity * Time.deltaTime;
            moveDirection = lastMoveDirection;
            isFalling = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", isFalling);
            animator.SetBool("isMoving", false);
        }
       
        /*
            Horizontal movement based on camera
        */
        Vector3 cameraForward = Camera.forward;
        Vector3 cameraRight = Camera.right; 
        moveDirection.y = 0; 
        moveDirection.Normalize();

        //an efficient edit would be to check for zeroes before doing other calculations
        moveDirection.x = Input.GetAxis("Horizontal") * speed;
        moveDirection.z = Input.GetAxis("Vertical") * speed;

        moveDirection = (cameraForward * moveDirection.z + cameraRight * moveDirection.x).normalized * speed;

        if (moveDirection.magnitude >= 0.1f && !isHit) 
        {

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
       
            animator.SetBool("isMoving", true);

            //play the running sound
            if (runSoundCoolDown > .27 && !isJumping && !isFalling)
            {
                src.clip = runSound;
                src.Play();
                runSoundCoolDown = 0;
            }
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

        //time track the running sound
        runSoundCoolDown += Time.deltaTime;
    }

    /*
        All collisions handler
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
                    src.clip = jumpSound;
                    src.Play();
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
            Getting hit by enemies
        */
        if (hit.gameObject.CompareTag("enemy"))
        {
            animator.SetBool("isHit", true);
            src.clip = hurtSound;
            src.Play();
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
    
    /*
    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Win"))
        {
            //Debug.Log("Hit.");
            timer.End();
        }
    }
    */
}

