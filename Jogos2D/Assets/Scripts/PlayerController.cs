using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public string playerInput;
    public float speed = 3.0f;
    public float gravity = 10.0f;
    public float jumpForce = 14.0f;
    public GameObject HookPrefab;
    public Transform HookPosition;

    private float verticalVelocity = 0;
    private bool canJump = true;
    private bool jump = false;

    private CharacterController controller;
    private Animator animator;

    void Start() {
        controller = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
    }

    void Update() {
        HandleMovement();
        HandleHook();
        HandleHooked();
    }

    private bool hookReleased = false;
    public GameObject lineRenderer;
    private GameObject hook;
    private void HandleHook() {
        if (hookReleased==false && Input.GetButtonDown("Action_" + playerInput)) {
            hook = Instantiate(HookPrefab) as GameObject;
            hook.transform.position = HookPosition.position;
            Rigidbody hookRigidbody = hook.GetComponent<Rigidbody>();
            HookScript hookScript = hook.GetComponent<HookScript>();
            hookScript.player = this.transform;
            hookRigidbody.velocity = new Vector3(this.transform.rotation.y / 0.7f, 0.2f, 0f) * 15;
            lineRenderer.gameObject.SetActive(true);
            lineRenderer.GetComponent<LineRendererCode>().point2 = hook.transform;
            
            hookReleased = true;
            Invoke("DestroyHook", 0.5f);
            Invoke("EnableHook", 1f);
        }
    }

    private void EnableHook() {
        hookReleased = false;
    }

    public void DestroyHook() {
        lineRenderer.gameObject.SetActive(false);
        Destroy(hook);
    }

    private float hookedTime = 0f;
    private Vector3 hookedVector;
    public void Hooked (Vector3 hookingVector) {
        hookedVector = hookingVector * 5f;
        hookedTime = 1f;
    }

    private void HandleHooked() {
        if (hookedTime > 0f) {
            hookedTime -= Time.deltaTime;
            controller.Move(hookedVector * Time.deltaTime);
        }
    }

    private void HandleMovement() {

        if (Input.GetAxis("Horizontal_" + playerInput) < -0.33f) {
            this.transform.rotation = new Quaternion(0f, -0.7f, 0f, 0.7f);
        } else if (Input.GetAxis("Horizontal_" + playerInput) > 0.33f) {
            this.transform.rotation = new Quaternion(0f, 0.7f, 0f, 0.7f);
        }

        if (controller.isGrounded) {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump_" + playerInput)) {
                verticalVelocity = jumpForce;
            }
        } else {
            verticalVelocity -= gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump_" + playerInput)) {
                verticalVelocity = -jumpForce;
            }
        }

        animator.SetBool("is_moving", Input.GetAxis("Horizontal_" + playerInput) != 0);

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (Input.GetAxis("Horizontal_" + playerInput) * speed);
        moveVector.y = (verticalVelocity);
        moveVector.z = -this.transform.position.z * 2;
        controller.Move(moveVector * Time.deltaTime);
    }
}
