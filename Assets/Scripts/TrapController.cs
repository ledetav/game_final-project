using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour {

    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
                // Анимация закрывания капкана
                animator.SetTrigger("isClosed");
                // Задержка для анимации закрывания капкана
                StartCoroutine(TrapDelay());
                animator.SetTrigger("isOpened");
        }
    }

    IEnumerator TrapDelay(){
        yield return new WaitForSeconds(1f);
    }
}