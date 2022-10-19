using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float inputX;
    private float inputY;
    private Vector2 movementInput;
    private Animator[] animators;
    private bool isMoving;
    private bool gameEnd;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
        gameEnd = false;
    }
    private void OnEnable()
    {
        EventHandler.GameOverEvent += OnGameOverEvent;
    }
    private void OnDisable()
    {
        EventHandler.GameOverEvent -= OnGameOverEvent;
    }
    private void OnGameOverEvent()
    {
        gameEnd = true;
    }
    private void Update()
    {
        PlayerInput();
        SwitchAnimation();
    }
    private void FixedUpdate()
    {
        if(!gameEnd)
        {
            Movement();
        }
    }


    private void PlayerInput()//获取人物移动方向
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if(inputX != 0 && inputY != 0)
        {
            inputX = inputX*0.6f;
            inputY = inputY*0.6f;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            inputX = inputX*0.5f;
            inputY = inputY*0.5f;
        }
        movementInput = new Vector2(inputX,inputY);
        isMoving = movementInput != Vector2.zero;
    }
    private void Movement()//实现人物移动
    {
        rb.MovePosition(rb.position + movementInput*speed*Time.deltaTime);
    }
    private void SwitchAnimation()
    {
        foreach(var anim in animators)
        {
            anim.SetBool("IsMoving",isMoving);
            if(isMoving)
            {
                anim.SetFloat("InputX",inputX);
                anim.SetFloat("InputY",inputY);
            }
        }
    }
}
