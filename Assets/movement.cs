using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    private bool jumpable = true;

    public float drag;

    public float jumpspeed;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    void OnCollisionEnter(Collision collision)
    {
        jumpable = true;
    }
    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");

        Vector2 vel = new Vector2 (moveHorizontal * speed, rb2d.velocity.y);
        vel.x *= 1.0f - drag;
        vel.y *= 1.0f - drag;

        rb2d.velocity = vel;
        if (Input.GetKeyDown("space"))
        {
            rb2d.AddForce(new Vector2(0, jumpspeed), ForceMode2D.Impulse);
            jumpable = false;
        }
    }
}
