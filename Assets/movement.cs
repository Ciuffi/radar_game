using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState { Idle, Jumping, Falling };

public class movement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    private float jumpTime = 0.5f;
    private PlayerState myState;
    

    public float drag;

    [Tooltip("Kenrick is chimpin out")]
    [SerializeField]
    private float jumpspeed;
    // Start is called before the first frame update
    void Start(){
        rb2d = GetComponent<Rigidbody2D> ();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        jumpTime = 0.5f;
        myState = PlayerState.Idle;
    }

    void FixedUpdate(){
        float moveHorizontal = Input.GetAxis ("Horizontal");
    
        Vector2 vel = new Vector2 (moveHorizontal * speed, rb2d.velocity.y);
        vel.x *= 1.0f - drag;
        vel.y *= 1.0f - drag;

        rb2d.velocity = vel;
        // Farther away from the ground you are, the less push it has
        // To a limit
        if (Input.GetKeyDown(KeyCode.Space) && myState == PlayerState.Idle){
            myState = PlayerState.Jumping;
            rb2d.AddForce(new Vector2(0, jumpspeed), ForceMode2D.Impulse);
            jumpTime -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && myState == PlayerState.Jumping){
            rb2d.AddForce(new Vector2(0, jumpspeed * Mathf.Max(0f, jumpTime) / 0.5f), ForceMode2D.Impulse);
            jumpTime -= Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space) && myState == PlayerState.Jumping) {
            myState = PlayerState.Falling;
        }
    }
}
