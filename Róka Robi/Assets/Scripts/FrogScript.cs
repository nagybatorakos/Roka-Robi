using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrogScript : Enemy

{
    private enum State { idle, jumping, falling}
    private State state = State.idle;

    private float leftCap;
    private float rightCap;
    private Collider2D coll;
    //private Rigidbody2D rb;

    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private bool enabled;
    [SerializeField] private LayerMask ground;

    private bool facingLeft = true;
    
    protected override void Start()
    {
        base.Start();
        GetPos();
        coll = GetComponent<Collider2D>();
        //rb = GetComponent<Rigidbody2D>();
        
    }

    
    void Update()
    {
        
        AnimationState();
        anim.SetInteger("state", (int)state);
        //Movement();
    }

    private void GetPos()
    {
        leftCap = GameObject.Find("WaypointLeft").transform.position.x;
        rightCap = GameObject.Find("WaypointRight").transform.position.x;
        Debug.Log(leftCap);
        Debug.Log(rightCap);
    }

    private void Movement()
    {
        if (enabled)
        {
            if (facingLeft)
            {
                if (transform.position.x > leftCap)
                {
                    if (coll.IsTouchingLayers(ground))
                    {
                        rb.velocity = new Vector2(jumpLength * -1, jumpHeight);
                        state = State.jumping;
                    }
                }
                else
                {
                    facingLeft = false;
                    transform.localScale = new Vector2(-1, 1);
                }
            }

            else if (facingLeft == false)
            {
                if (transform.position.x < rightCap)
                {
                    if (coll.IsTouchingLayers(ground))
                    {

                        rb.velocity = new Vector2(jumpLength, jumpHeight);
                        state = State.jumping;
                    }
                }
                else
                {
                    facingLeft = true;
                    transform.localScale = new Vector2(1, 1);
                }
            }
        }
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .2f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else
        {
            state = State.idle;
        }
    }




}
