using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private GameObject settingsMenu;

    void Start(){
        settingsMenu.SetActive(false);
    }

    public void StartGame(){
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void Settings(){
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void ExitGame(){
        Application.Quit();
    }
}