using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Hangok;

public class PlayerController : MonoBehaviour
{
    //Start() variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    

    //FSM
    private enum State { idle, running, jumping, falling, hurt}
    private State state = State.idle;

    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float PlayerSpeed = 5;
    [SerializeField] private float JumpHeight = 8;
    [SerializeField] private int cherries;
    [SerializeField] private Text cherryText;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource footstep;

    [SerializeField] private Audio audio; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        //audio = GetComponent<Audio>();
        //Hangok.Audio audio = new Hangok.Audio();
        audio = GameObject.Find("Audio").GetComponent<Audio>();
        audio.Music();
    }


    void Update()
    {
        if(state != State.hurt)
        {
        Movement();
        }

        AnimationState();
        anim.SetInteger("state", (int)state);

        
    }

    private void Movement()
        {
        float hDirection = Input.GetAxis("Horizontal");
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(PlayerSpeed * -1, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }

        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(PlayerSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }

        else
        {

        }

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
        state = State.jumping;
        audio.JumpSound();
        
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
                audio.Landing();
            }
        }
        else if(state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > .2f)
        {
            state = State.running;
        }
        else 
        {
            state = State.idle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            cherries++;
            cherryText.text = cherries.ToString();
            audio.CherrySound();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(state== State.falling)
            {
                enemy.JumpedOn();
                Jump();
                audio.EnemyDeath();
            }
            else
            {
                state = State.hurt;
                audio.Hurt();
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }

            }
            
        }
    }
    private void Footstep()
    {
        if (coll.IsTouchingLayers(ground))
        {
            footstep.Play();
        }
    }

}