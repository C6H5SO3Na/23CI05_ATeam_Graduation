using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// �A�j���[�V������ς���
    /// </summary>
    /// <param name="state">���݂̏��</param>
    public void ChangeAnimation(PlayerStateMachine state)
    {
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
        else if (state is PlayerClearState)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Jump", false);
        }
        else if (state is PlayerDeadState)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Jump", false);
            animator.SetTrigger("Dead");
        }
        else if (state is PlayerThrowState)
        {
            animator.SetTrigger("Throw");
        }
    }
}
