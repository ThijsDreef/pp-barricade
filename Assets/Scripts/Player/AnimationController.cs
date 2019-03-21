using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    private Animator animator;
    public Animator Animator { get { return animator; } set { Animator = value; } }
    private Pawn pawn;
    private void Start() {
        animator = GetComponent<Animator>();
        pawn = GetComponent<Pawn>();
        pawn.StartMoving += StartmoveAnim;
        pawn.StopMoving += StartIdleAnim;
        pawn.StartAttack += StartAttackAnim;
    }

    private void StartmoveAnim() {
        //animator.SetTrigger("Walk");
        animator.SetBool("IDLE", false);
        animator.SetBool("WALKING", true);
    }

    private void StartIdleAnim() {
        //animator.SetTrigger("Idle");
        animator.SetBool("IDLE", true);
        animator.SetBool("WALKING", false);
    }

    private void StartAttackAnim() {
        animator.SetTrigger("Attack");
    }

    private void PauseAnim() {
        animator.enabled = false;
    }

    private void ResumeAnim() {
        animator.enabled = true;
    }
}
