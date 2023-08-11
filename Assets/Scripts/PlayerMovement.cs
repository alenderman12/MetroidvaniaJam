using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float maxJumpTime;
    private float jumpTime;
    private bool isGrounded;
    private bool jumpButtonReleased;

    private Rigidbody2D rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxisRaw("Horizontal") * moveSpeed, rigidBody.velocity.y);
        if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
        {
            jumpButtonReleased = false;
            jumpTime = 0;
            Move(Input.GetAxisRaw("Horizontal") * moveSpeed, jumpStrength);
        }
        if (Input.GetKey(KeyCode.Z) && jumpTime <= maxJumpTime && !jumpButtonReleased)
        {
            Move(Input.GetAxisRaw("Horizontal") * moveSpeed, jumpStrength);
            jumpTime += Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Z) || jumpTime >= maxJumpTime)
        {
            jumpButtonReleased = true;
            rigidBody.gravityScale = 3;
        }
    }

    private void Move(float x, float y)
    {
        rigidBody.velocity = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "DownAttack")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name != "DownAttack")
        {
            isGrounded = false;
        }
    }
}
