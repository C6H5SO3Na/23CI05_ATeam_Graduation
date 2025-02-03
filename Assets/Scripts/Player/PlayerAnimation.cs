using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerController player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = transform.parent.parent.GetComponent<PlayerController>();
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

    /// <summary>
    /// �A�j���[�V�����̑��x���X�V
    /// </summary>
    /// <param name="state">���݂̏��</param>
    public void UpdateAnimationSpeed(PlayerStateMachine state)
    {
        if (state is PlayerMoveState)
        {
            var moveDirection = new Vector2(player.GetMoveDirection().x, player.GetMoveDirection().z);
            float dir = moveDirection.magnitude;
            animator.speed = dir;
        }
        else
        {
            animator.speed = 1f;
        }
    }
}
