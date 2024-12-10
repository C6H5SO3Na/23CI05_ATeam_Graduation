using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerStateMachine state;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        state = transform.parent.parent.GetComponent<PlayerController>().State;
        if (state is PlayerIdleState)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Jump", false);
        }
        else if (state is PlayerMoveState)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Jump", false);
        }
        else if (state is PlayerJumpState)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Jump", true);
        }
    }
}
