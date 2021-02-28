using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingController : Controller {

    //movement information
    Vector3 walkVelocity;
    private float adjVertVelocity;
    private Vector3 adjWallJumpVelocity;
    private float jumpPressTime;
    private float wallJumpPressTime;
    

    //settings
    [SerializeField]
    private float walkSpeed = 3f;
    [SerializeField]
    private float jumpSpeed = 6f;
    [SerializeField]
    private float wallJumpSpeed = 100f;
    [SerializeField]
    private Animator animator;

    public override void ReadInput(InputData data)
    {
        ResetMovementToZero();

        //set vertical movement
        if (data.axes[0] != 0f)
        {
            walkVelocity += Vector3.forward * data.axes[0];
        }

        //set horizontal movement
        if (data.axes[1] != 0f)
        {
            walkVelocity += Vector3.right * data.axes[1]; //right positive sens
        }

        //set vertical jump
        if (data.buttons[0])
        {
            if (jumpPressTime == 0f)
            {
                if (Grounded())
                {
                    adjVertVelocity = jumpSpeed;
                }

            }
            jumpPressTime += Time.deltaTime;
        }
        else
        {
            jumpPressTime = 0f;
        }

        //probleme qd j'appuie sur aucun touche je 
        if (data.buttons[1]) // attack
        {
            Debug.Log("ATTACK");
            //animator.Play("Armature|slash");
            animator.Play("Armature|slash", -1, 0f);
        }

        if (data.buttons[2])
        {
            if (wallJumpPressTime == 0f)
            {
                Vector3 walled = Walled();
                if (walled != Vector3.zero)
                {
                    adjVertVelocity = jumpSpeed;
                    adjWallJumpVelocity = walled * wallJumpSpeed;
                    Debug.Log(adjWallJumpVelocity);
                }

            }
            wallJumpPressTime += Time.deltaTime;
        }
        else
        {
            wallJumpPressTime = 0f;
        }
        newInput = true;
    }

    //method that will look below our character and see if there is a collider
    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.3f);
    }

    // si contacte d'un wall renvoie le vector opposer
    Vector3 Walled()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, coll.bounds.extents.y + 0.1f))
        {
            return Vector3.back;
        }
        if (Physics.Raycast(transform.position, Vector3.back, coll.bounds.extents.y + 0.1f))
        {
            return Vector3.forward;
        }
        if (Physics.Raycast(transform.position, Vector3.left, coll.bounds.extents.y + 0.1f))
        {
            return Vector3.right;
        }
        if (Physics.Raycast(transform.position, Vector3.right, coll.bounds.extents.y + 0.1f))
        {
            return Vector3.left;
        }
        if (Physics.Raycast(transform.position, Vector3.forward+Vector3.left, coll.bounds.extents.y + 0.1f))
        {
            return Vector3.back+Vector3.right;
        }
        if (Physics.Raycast(transform.position, Vector3.forward+Vector3.right, coll.bounds.extents.y + 0.1f))
        {
            return Vector3.back+Vector3.left;
        }
        if (Physics.Raycast(transform.position, Vector3.back+Vector3.left, coll.bounds.extents.y + 0.1f))
        {
            return Vector3.forward+Vector3.right;
        }
        if (Physics.Raycast(transform.position, Vector3.back+Vector3.right, coll.bounds.extents.y + 0.1f))
        {
            return Vector3.forward+Vector3.left;
        }
        return Vector3.zero;
    } 

    private void FixedUpdate()
    {
        //if (rb.velocity.y < 0)
        //{
        //    //playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //    rb.AddForce(Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        //}
        //else if (rb.velocity.y > 0 && !data.buttons[0] == true)
        //{
        //    //playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //    rb.AddForce(Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        //}
    }

    void LateUpdate()
    {
        if (!newInput)
        {
            ResetMovementToZero();
            jumpPressTime = 0f;
        }
        walkVelocity = rb.gameObject.transform.rotation * walkVelocity * moveSpeed;
        rb.velocity = new Vector3(walkVelocity.x, rb.velocity.y + adjVertVelocity , walkVelocity.z) + adjWallJumpVelocity;
        newInput = false;
    }

    void ResetMovementToZero()
    {
        walkVelocity = Vector3.zero;
        adjVertVelocity = 0f;
        adjWallJumpVelocity = Vector3.zero;
    }
}
