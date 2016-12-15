using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public string playerInput;
    public float speed = 3.0f;
    public float gravity = 10.0f;
    public float jumpForce = 14.0f;
    public GameObject HookPrefab;
    public Transform HookPosition;
    public GameObject JumpParticle;
    public GameObject DescendParticle;

    private float verticalVelocity = 0;
    private bool canJump = true;
    private bool jump = false;

    private CharacterController controller;
    private Animator animator;

    private bool step = true;
    float audioStepLengthWalk = 0.45f;

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

            hookRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal_" + playerInput), Input.GetAxis("Vertical_" + playerInput), 0f) * 20;

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

    IEnumerator WaitForFootSteps(float stepsLength) { step = false; yield return new WaitForSeconds(stepsLength); step = true; }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (controller.isGrounded && controller.velocity.magnitude < 7 && controller.velocity.magnitude > 5 && hit.gameObject.tag == "Untagged" && step == true) {
            AudioSource stepAudioSource = GameObject.Find("passo audio").GetComponent<AudioSource>();
            stepAudioSource.Play();
            StartCoroutine(WaitForFootSteps(audioStepLengthWalk));
        }

        if (hit.gameObject.name.Contains("Treasure")) {
            Win();
        }
    }

    void Win() {
        GameObject managers = GameObject.Find("Managers");
        managers.GetComponent<CollisionManager>().EndGame(this.gameObject.name);
        Camera.main.transform.LookAt(this.transform);
        Camera.main.orthographic = false;
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
                AudioSource jumpAudioSource = GameObject.Find("pulo audio").GetComponent<AudioSource>();
                jumpAudioSource.Play();

                GameObject jumpParticle = Instantiate(JumpParticle) as GameObject;
                jumpParticle.transform.position = this.transform.position;
                jumpParticle.transform.SetParent(this.transform);
                verticalVelocity = jumpForce;
                Destroy(jumpParticle, 0.25f);
            }
        } else {
            verticalVelocity -= gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump_" + playerInput)) {
                GameObject descendParticle = Instantiate(DescendParticle) as GameObject;
                descendParticle.transform.position = this.transform.position;
                descendParticle.transform.SetParent(this.transform);
                verticalVelocity = -jumpForce;
                Destroy(descendParticle, 0.25f);
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
