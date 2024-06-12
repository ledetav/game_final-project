using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    private int numberOfCheckpoint;
    private Vector3 spawnPos;

    public Vector3 SpawnPos { get => spawnPos; set => spawnPos = value; }

    void Start(){
        numberOfCheckpoint = 1;
        spawnPos = GameObject.Find("Checkpoint1").transform.position;
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Checkpoint"){
            if(numberOfCheckpoint != Convert.ToInt16(collision.gameObject.name.Substring(collision.gameObject.name.Length-1))){
                // Устанавливаем позицию спавна на чeкпоинте с названием Checkpoint1 со смещением по оси Y
                spawnPos = collision.gameObject.transform.position;
                Debug.Log(spawnPos);
                numberOfCheckpoint = Convert.ToInt16(collision.gameObject.name.Substring(collision.gameObject.name.Length-1));
            }
        }
    }
}