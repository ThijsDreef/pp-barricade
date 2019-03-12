using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    private Animator animator;
    private Pawn pawn;
    void Start() {
        animator = GetComponent<Animator>();
        pawn = GetComponent<Pawn>();
        pawn.StartMoving += StartmoveAnim;
        pawn.StopMoving += StartIdleAnim;
    }

    void StartmoveAnim() {
        animator.SetTrigger("Walk");
    }

    void StartIdleAnim() {
        animator.SetTrigger("Idle");
    }

    void StartAttackAnim() {
        animator.SetTrigger("Attack");
    }
}
