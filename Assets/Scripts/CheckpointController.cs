using UnityEngine;

public class CheckpointController : MonoBehaviour {
    private bool isTaken;
    public bool IsTaken { get => isTaken; set {if (!value) isTaken = value; } }

    void Start() {
        isTaken = true;
    }

    void Update(){

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player"){
            isTaken = true;
            CheckpointController[] checkpoints = GameObject.Find("CHECKPOINTS").GetComponentsInChildren<CheckpointController>();
            foreach (CheckpointController script in checkpoints) {
                script.IsTaken = false;
            }
        }
    }
}