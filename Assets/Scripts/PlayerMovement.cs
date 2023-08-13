using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    Moving,
    Attacking,
    Idle,
    Interacting,
    Knockback
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float knockbackTime;
    public static PlayerState playerState = PlayerState.Moving;
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
        if (playerState == PlayerState.Moving)
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
    }

    private void Move(float x, float y)
    {
        rigidBody.velocity = new Vector2(x, y);
    }

    public void Knockback(Vector3 knockbackOrigin, float knockbackForce)
    {
        playerState = PlayerState.Knockback;
        rigidBody.AddForce((transform.position - knockbackOrigin)* knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(KnockbackCo());
    }

    private IEnumerator KnockbackCo()
    {
        yield return new WaitForSeconds(knockbackTime);
        playerState = PlayerState.Moving;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
