using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float maxJumpTime;
    private float jumpTime;
    private float lastYposition;
    private bool isGrounded;

    private Rigidbody2D rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        lastYposition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxisRaw("Horizontal") * moveSpeed, rigidBody.velocity.y);
        if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
        {
            jumpTime = 0;
            Move(Input.GetAxisRaw("Horizontal") * moveSpeed, jumpStrength);
        }
        if (Input.GetKey(KeyCode.Z) && jumpTime <= maxJumpTime)
        {
            Move(Input.GetAxisRaw("Horizontal") * moveSpeed, jumpStrength);
            jumpTime += Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Z) || jumpTime >= maxJumpTime)
        {
            rigidBody.gravityScale = 3;
        }
    }

    private void Move(float x, float y)
    {
        rigidBody.velocity = new Vector2(x, y);
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            rigidBody.gravityScale = 1;
        }
        isGrounded = (lastYposition == transform.position.y);
        lastYposition = transform.position.y;
    }
}
