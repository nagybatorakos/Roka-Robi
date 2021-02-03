using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : Enemy
{
    [SerializeField] private float force;

    private Collider2D coll;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Up()
    {
        rb.velocity = new Vector2(rb.velocity.x, force);
    }

    private void Down()
    {
        rb.velocity = new Vector2(rb.velocity.x, force*-1);
    }
}
