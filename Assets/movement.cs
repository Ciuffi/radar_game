using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState { Idle, Jumping, Falling };
enum MoveState { Idle, Moving};
public class movement : MonoBehaviour
{
    //Radar Variables
    public float HorizontalSpeed;
    public float HorizontalDrag;
    public float JumpForce;

    //No Radar variables
    private PlayerState myState;
    private MoveState myMoveState;
    private Rigidbody2D rb2d;
    private float jumpTimer;

    // Start is called before the first frame update
    void Start(){
        rb2d = GetComponent<Rigidbody2D> ();
        myState = PlayerState.Idle;
        myMoveState = MoveState.Idle;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        jumpTimer = 0.4f;
        myState = PlayerState.Idle;
    }

    void FixedUpdate(){
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        Vector2 vel;
        if((rb2d.velocity.x <= 0.01 && rb2d.velocity.x > 0) || 
            rb2d.velocity.x >= -0.01 && rb2d.velocity.x < 0){
            myMoveState = MoveState.Idle;
        }
        if (moveHorizontal > 0 || moveHorizontal < 0){
            myMoveState = MoveState.Moving;
            vel = new Vector2 (moveHorizontal * HorizontalSpeed, rb2d.velocity.y);
        }else if (myMoveState == MoveState.Moving) {
            vel = new Vector2 (rb2d.velocity.x *  (1.0f - HorizontalDrag), rb2d.velocity.y);
        }else{
            vel = new Vector2 (moveHorizontal * HorizontalSpeed, rb2d.velocity.y);
        }
        
        rb2d.velocity = vel;
        // Farther away from the ground you are, the less push it has
        // To a limit
        if (Input.GetKeyDown(KeyCode.Space) && myState == PlayerState.Idle){
            myState = PlayerState.Jumping;
            rb2d.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            jumpTimer -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && myState == PlayerState.Jumping){
            rb2d.AddForce(new Vector2(0, JumpForce * Mathf.Max(0f, jumpTimer) / 0.5f), ForceMode2D.Impulse);
            jumpTimer -= Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space) && myState == PlayerState.Jumping) {
            myState = PlayerState.Falling;
        }
    }
}
