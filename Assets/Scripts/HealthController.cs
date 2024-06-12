using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    private GameObject[] healthObjects;
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private GameObject prefabHealthObject;

    private PlayerController playerController;
    private Animator animator;
    private Rigidbody2D rb;

    private int health = 0;
    private int lastScoreForHealth = 0;

    void Start(){
        health = maxHealth;
        healthObjects = new GameObject[maxHealth];
        UpdateHealthDisplay(maxHealth);

        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        addHealth();
    }

    private void UpdateHealthDisplay(int health) {
        // Заново создаем объекты здоровья
        float sizeHealthOnjectWithOffset = 0.2f;
        float size = maxHealth * sizeHealthOnjectWithOffset;
        for(int i = 0; i < health; i++){
            healthObjects[i] = Instantiate(prefabHealthObject, Vector3.zero, Quaternion.identity);
            healthObjects[i].transform.SetParent(transform, false);
            healthObjects[i].transform.position = new Vector3((transform.position.x - size/2 + 0.02f) + i * sizeHealthOnjectWithOffset, transform.position.y + 0.15f, transform.position.z);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Trap" || collision.gameObject.tag == "Enemy") {
            Debug.Log(health + "health ");
            if(health > 0){
                health -= 1;
                Destroy(healthObjects[health]);
                StartCoroutine(HurtDelay());
            } 
            if(health <= 0){
                health = maxHealth;
                StartCoroutine(HurtDelay());
                StartCoroutine(DeathAndRespawnRoutine());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "PitWall"){
            if(health > 0){
                health -= 1;
                Destroy(healthObjects[health]);
                StartCoroutine(HurtDelay());
                gameObject.transform.position = GetComponent<SpawnController>().SpawnPos;
            } 
            if(health <= 0){
                health = maxHealth;
                StartCoroutine(HurtDelay());
                StartCoroutine(DeathAndRespawnRoutine());
            }
        }
    }

    private void addHealth(){
        int currentScore = gameObject.GetComponent<PickUpController>().Score;
        if(health < maxHealth && currentScore % 3 == 0 && currentScore != 0 && (currentScore - lastScoreForHealth) >= 3){
            // Удалить текущий дисплей здоровья перед созданием нового объекта
            for (int i = 0; i < healthObjects.Length; i++) {
                if (healthObjects[i] != null) Destroy(healthObjects[i]);
            }
            
            health++; // увеличить здоровье перед созданием дисплея

            // Пересоздаем дисплей здоровья
            UpdateHealthDisplay(health);
            
            lastScoreForHealth = currentScore;
        }
    }

    private IEnumerator HurtDelay(){
        animator.SetTrigger("isHurt");
        yield return new WaitForSeconds(1f); // Задержка 1 секунда
    }

    private IEnumerator DeathAndRespawnRoutine() {
        playerController.isControlEnabled = false;
        
        gameObject.GetComponents<AudioSource>()[2].Play();
        animator.SetBool("isDead", true);

        // Задержка перед респавном
        yield return new WaitForSeconds(2f); // Задержка 2 секунды
        animator.SetBool("isDead", false);
        
        // Респавн персонажа
        gameObject.transform.position = GetComponent<SpawnController>().SpawnPos;

        // Восстановление здоровья
        UpdateHealthDisplay(health);

        playerController.isControlEnabled = true;
    }
}