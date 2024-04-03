using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    
    private CharacterController characterController;




    public float sprintSpeed = 4f;
    public float walkSpeed = 2f;
    public float mouseSensitivity = 1f;

    private bool isSprint;

    private Animator animator;

    private float horizontalInput;

    private float verticalInput;

    private Vector3 moveDirection;

    public bool alive = true;

    protected  void Awake()
    {
    
    
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        alive = true;

    }

    void Update()
    {
    
        Move();


    }

    public void Move(){
        animator.speed = 1f;
        
        // if (Input.GetKeyDown(KeyCode.R) && !alive ){
        //     GameManager.Instance.UpdateGameState(GameState.Restart);
        //     Debug.Log("Restarting the game");
        //     return;
        // }
        if (!alive) return;
        
        verticalInput = Input.GetAxisRaw("Vertical");
        isSprint = Input.GetKey(KeyCode.LeftShift);

        moveDirection = new Vector3(0, 0,  verticalInput);

        if (moveDirection != Vector3.zero){
            Rotate();
            if (verticalInput > 0 && isSprint){
                Sprint();
            } else {
                if (verticalInput < 0){
                    WalkBack();
                } else {
                    Walk();
                }
            }
        } else {
            Idle();
        }
        characterController.Move(moveDirection * Time.deltaTime);
    }


    public void Walk(){
        animator.SetFloat("Speed", 0.5f, dampTime: 0.1f, Time.deltaTime);

    }
    public void WalkBack(){
        animator.SetFloat("Speed", 0f, dampTime: 0.1f, Time.deltaTime);

    }

    public void Sprint(){
        animator.SetFloat("Speed", 1f, dampTime: 0.1f, Time.deltaTime);

    }

    public void Idle(){
        animator.SetFloat("Speed", 0.25f, dampTime: 0.1f, Time.deltaTime);
    }
    public void Rotate(){
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * horizontalInput * mouseSensitivity);
    }

    public void Die(){
        animator.SetTrigger("Die");
        alive = false;
        GameManager.Instance.Canvas.SetActive(true);
    }

   
 
}
