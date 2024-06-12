using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private PatrolPointsController patrolPointsController;
    private AudioSource attackSound;
    private AudioSource hurtAndDeathSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        patrolPointsController = GetComponent<PatrolPointsController>();
        attackSound = GetComponents<AudioSource>()[0];
        hurtAndDeathSound = GetComponents<AudioSource>()[1];
    }

    public void TakeDamage()
    {
        StartCoroutine(DelayBetweenHurtAndDeath());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                animator.SetTrigger("isAttacking");
                attackSound.Play();
            }
        }
    }

    IEnumerator DelayBetweenHurtAndDeath(){
        patrolPointsController.IsPatrolling = false;
        animator.SetTrigger("isHurt");
        hurtAndDeathSound.Play();
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isDead", true);
        // Disable enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        rb.isKinematic = true;
        // Delete enemy after 5 seconds
        Destroy(gameObject, 3f);
    }
}