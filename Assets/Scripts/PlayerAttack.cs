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
        if (Input.GetKey(KeyCode.X))
        {
            animator.SetBool("attackPressed", true);
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                animator.SetFloat("xDirection", Input.GetAxisRaw("Horizontal"));
            }
            StartCoroutine(WaitForAnimation());
        }
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForEndOfFrame();
        animator.SetBool("attackPressed", false);
    }
}
