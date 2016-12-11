using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour {
    public string playerInput;
    public float speed = 3.0f;
    public float jumpForce = 5.0f;
    private bool canJump = true;
    private bool jump = false;

    private Rigidbody2D rigidBody;
    private Collider2D collider;
    public LayerMask ground;
    void Start() {
        rigidBody = this.GetComponent<Rigidbody2D>();
        collider = this.GetComponent<Collider2D>();
    }

    void Update() {
        if (Input.GetButtonDown("Jump_" + playerInput) && canJump) {
            jump = true;
            canJump = false;
        }
    }
	
	void FixedUpdate () {
        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal_" + playerInput) *speed, rigidBody.velocity.y);

        if (Physics2D.IsTouchingLayers(collider, ground)) {
            canJump = true;
        }

        if (jump) {
            canJump = false;
            jump = false;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }
}
