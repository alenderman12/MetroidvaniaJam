using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            animator.SetFloat("xDirection", Input.GetAxisRaw("Horizontal"));
        }
        if (Input.GetKey(KeyCode.X))
        {
            animator.SetBool("attackPressed", true);
            Invoke("SetAnimationFalse", .1f);
        }
    }

    private void SetAnimationFalse()
    {
        animator.SetBool("attackPressed", false);
    }
}
