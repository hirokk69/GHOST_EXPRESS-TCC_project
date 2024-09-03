using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int moveSpeed;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundRayTransform;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private int numJumps;
    [SerializeField] private int jumpForce;
    private int jumpsLeft;
    private float jumpTimer;

    [SerializeField] private Animator anim;
    [SerializeField] private Transform playerVisual;

    private Vector3 playerScale;

    private const string WALK_PARAM = "IsWalking";
    private const string ATTACK_PARAM = "IsAttacking";

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerScale = playerVisual.transform.localScale;
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.Linecast(transform.position, groundRayTransform.position, groundLayer);

        if (isGrounded == true && jumpTimer >= 0.5f)
        {
            jumpsLeft = numJumps;
        }

        jumpTimer += Time.fixedDeltaTime;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        transform.position = new Vector3(transform.position.x + x * Time.deltaTime * moveSpeed,
            transform.position.y, transform.position.z);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsLeft > 0)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpForce);
                jumpsLeft--;
                jumpTimer = 0;
            }
        }

        anim.SetBool(WALK_PARAM, x != 0);

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger(ATTACK_PARAM);
        }

        if (x > 0)
        {
            playerVisual.transform.localScale = playerScale;
        }
        else if (x < 0)
        {
            playerVisual.transform.localScale = new Vector3(-playerScale.x, playerScale.y, playerScale.z);
        }
    }
}
