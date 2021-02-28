using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class J1Controls : MonoBehaviour
{
    PlayerControls controls;
    
    //movement information
    Vector3 walkVelocity;
    private float adjVertVelocity;
    private Vector3 adjWallJumpVelocity;
    private float jumpPressTime;
    private float wallJumpPressTime;
    private Vector2 move;

    //settings
    [SerializeField]
    private float walkSpeed = 3f;
    [SerializeField]
    private float jumpSpeed = 6f;
    [SerializeField]
    private float wallJumpSpeed = 100f;
    [SerializeField]
    private float moveSpeed = 5f;
    
    //Composant
    [SerializeField] 
    private Collider coll;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody rb;


    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Awake(){
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => Jump();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    private void Jump()
    {
        if (Grounded())
        {
            //adjVertVelocity = jumpSpeed;
            rb.velocity += new Vector3(0,jumpSpeed,0); 
        }
    }
    
    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.3f);
    }
    
    void ResetMovementToZero()
    {
        walkVelocity = Vector3.zero;
        adjVertVelocity = 0f;
        adjWallJumpVelocity = Vector3.zero;
    }
    
    void LateUpdate()
    {
        Vector3 m = new Vector3(move.x,0,move.y)*Time.deltaTime;
        transform.Translate(m,Space.World);
        //walkVelocity = rb.gameObject.transform.rotation * walkVelocity * moveSpeed;
        //rb.velocity = new Vector3(walkVelocity.x, rb.velocity.y + adjVertVelocity , walkVelocity.z) + adjWallJumpVelocity;
        //newInput = false;
    }
}
