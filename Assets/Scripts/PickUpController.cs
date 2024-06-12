using UnityEngine;
using System.Collections;
using TMPro;

public class PickUpController : MonoBehaviour {

    [SerializeField]
    private TMP_Text PickupText;
    private int score = 0;

    public int Score { get => score; set => score = value; }

    private void Start(){
        PickupText.text += score;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Pickup") {
            score++;
            AudioSource sound = collision.gameObject.GetComponent<AudioSource>();
            if (sound != null) sound.Play();
            Destroy(collision.gameObject, 0.5f);
            PickupText.text = "Score: " + score;
        }
    }
}