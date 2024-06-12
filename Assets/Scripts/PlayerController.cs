using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jump;

    private bool isFlipping = true;
    private bool isGrounded = false;
    private bool canAttack = true;
    public bool isControlEnabled {get; set;} = true;

    private Rigidbody2D rb;
    private Animator animator;

    private AudioSource walkSound;
    private AudioSource jumpSound;
    private AudioSource attackSound;


    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        walkSound = GetComponents<AudioSource>()[0];
        jumpSound = GetComponents<AudioSource>()[1];
        attackSound = GetComponents<AudioSource>()[3];
    }

    void Update(){
        if(isControlEnabled){
            if(Input.GetKeyDown(KeyCode.Z) && canAttack){
                animator.SetTrigger("isAttacking");
                attackSound.Play();

                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1.0f);
                if (hit.collider != null && hit.collider.CompareTag("Enemy")) {
                    EnemyController enemy = hit.collider.GetComponent<EnemyController>();
                    if (enemy != null){
                        enemy.TakeDamage();
                    }
                }

                StartCoroutine(DelayBetweenAttacks());
            }
            
            if(isGrounded && Input.GetAxis("Jump") > 0){
                animator.SetTrigger("isJumping");
                jumpSound.Play();
                isGrounded = false;
                rb.AddForce(jump * Vector2.up, ForceMode2D.Impulse);
            }
            if((isFlipping && Input.GetAxis("Horizontal") < 0) || (!isFlipping && Input.GetAxis("Horizontal") > 0))
                Flip();
            transform.Translate(new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0));
            if(Input.GetAxis("Horizontal") != 0){
                if(!walkSound.isPlaying)
                    walkSound.Play();
                animator.SetBool("isWalking", true);
            } else {
                animator.SetBool("isWalking", false);
                walkSound.Stop();
            }
        }
    }

    private void Flip(){
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        isFlipping = !isFlipping;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        isGrounded = true;
    }

    IEnumerator DelayBetweenAttacks(){
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }
}