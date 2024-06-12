using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsController : MonoBehaviour
{
    private bool isFlipping = false;
    private bool stay = false;
    private float patrolSpeed = 2f;

    private Animator animator;
    private SpriteRenderer render;
    private AudioSource walkSound;

    public bool IsPatrolling {get; set;} = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        walkSound = GetComponents<AudioSource>()[2];
    }

    void Update(){
        if (IsPatrolling){
            if(!stay){
                animator.SetBool("isWalking", true);
                walkSound.Play();
                transform.Translate(new Vector3(isFlipping ? patrolSpeed : -patrolSpeed, 0, 0) * Time.deltaTime);
            } else {
                animator.SetBool("isWalking", false);
                walkSound.Stop();
            }
        }
    }

    private void Flip() {
        render.flipX = isFlipping;
        isFlipping = !isFlipping;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(!stay && IsPatrolling)
            StartCoroutine(stayDelay());
    }

    IEnumerator stayDelay() {
        stay = true;
        yield return new WaitForSeconds(3f);
        if(IsPatrolling){
            stay = false;
            Flip();
        }
    }
}