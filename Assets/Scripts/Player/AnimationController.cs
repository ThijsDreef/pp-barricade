using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    private Animator animator;
    private Pawn pawn;
    private void Start() {
        animator = GetComponent<Animator>();
        pawn = GetComponent<Pawn>();
        pawn.StartMoving += StartmoveAnim;
        pawn.StopMoving += StartIdleAnim;
    }

    private void StartmoveAnim() {
        animator.SetTrigger("Walk");
    }

    private void StartIdleAnim() {
        animator.SetTrigger("Idle");
    }

    private void StartAttackAnim() {
        animator.SetTrigger("Attack");
    }
}
