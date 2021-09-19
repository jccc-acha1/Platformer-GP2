using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    
    public float speed = 5f; // Players walk speed
    public float rotateSpeed = 950f; // Players flip rotation speed (y-axis)
    public float jumpSpeed = 60f; // Players jump speed
    public LayerMask groundLayer; // Ground layer for main collisions
    private Vector2 moveInput;

    bool isLookingLeft;
    bool isRotating;
    bool isGrounded;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() // Handle animations and jumping
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump")) {
            if (IsGrounded()) {
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                anim.SetBool("jump", true);
            }
        }

        if ((horz != 0 || vert != 0) && isGrounded) {
            if (anim.GetBool("jump") == false) {
                anim.SetBool("walk", true);
            }
        } else {
            anim.SetBool("walk", false);
        }
    }

    bool IsGrounded() // Use raycast to check for any ground layer within the ray
    { 
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.5f, groundLayer)) {
            //if (hit.collider.CompareTag("Ground")) {
            isGrounded = true;
            return true;
            //}
        }
        isGrounded = false;
        return false;
    }

    void OnCollisionEnter(Collision col) // I have better ways to do this, but bugs say otherwise
    { 
        if (col.collider.tag == "Ground" || col.collider.tag == "Platform") {
            isGrounded = true;
            anim.SetBool("jump", false);
        }
    }

    void FixedUpdate() // Handle actual transform and movement here
    {
        moveInput.x = Input.GetAxis("Horizontal"); // Yes I know these two are repetative
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();
        float horz = moveInput.x;
        float vert = moveInput.y;
        

        Vector3 move = new Vector3(horz, 0, vert);

        if (horz < 0 && !isLookingLeft) {
            StartCoroutine(Rotate(-1));
            isLookingLeft = true;
        } 
        else if (horz > 0 && isLookingLeft) {
            StartCoroutine(Rotate(1));
            isLookingLeft = false;
        }
        
        transform.position += move * Time.deltaTime * speed;
    }

    IEnumerator Rotate(float dir) // Rotate/flip coroutine
    {
        if (isRotating) {
            yield break;
        }
        isRotating = true;

        Vector3 curRot = transform.eulerAngles;
        Vector3 goalRot = new Vector3(curRot.x, curRot.y + (180f * dir), curRot.z);
        while (RotateCheck(goalRot)) { yield return null; }
        
    
        isRotating = false;
    }

    bool RotateCheck(Vector3 goalRot) // Check if the rotation has reached goal or not
    { 
        Quaternion goal =  Quaternion.Euler(goalRot);
        return goal != (transform.rotation = Quaternion.RotateTowards(transform.rotation, goal, rotateSpeed * Time.deltaTime));
    }
}
