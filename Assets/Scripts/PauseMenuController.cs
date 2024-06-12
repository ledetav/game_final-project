using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenuController : MonoBehaviour {

    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private AudioMixer soundsMixer;
    [SerializeField]
    private GameObject pauseMenuPanel;
    private AudioSource mainAudioSource;
    private PlayerController playerController;
    private bool isPlaying;
    private GameObject[] enemies;
    private GameObject[] pickups;
    private GameObject[] traps;
    void Start(){
        mainAudioSource = GetComponent<AudioSource>();
        isPlaying = true;
        pauseMenuPanel.SetActive(false);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        pickups = GameObject.FindGameObjectsWithTag("Pickup");
        traps = GameObject.FindGameObjectsWithTag("Trap");
        LoadParams();
    }

    void Update(){

        if(Input.GetKeyDown(KeyCode.Escape) && isPlaying){
            if(pauseMenuPanel != null){
                pauseMenuPanel.SetActive(true);
                mainAudioSource.Pause();
                isPlaying = false;
                SetIsPlay(isPlaying);
            }
        } else if(Input.GetKeyDown(KeyCode.Escape) && !isPlaying){
            if(pauseMenuPanel != null){
                pauseMenuPanel.SetActive(false);
                mainAudioSource.UnPause();
                isPlaying = true;
                SetIsPlay(isPlaying);
            }
        }
    }

    private void SetIsPlay(bool isPlaying){
        playerController.isControlEnabled = isPlaying;
        GameObject.Find("Player").GetComponent<Animator>().enabled = isPlaying;
        PausingAudioSources(GameObject.Find("Player"), isPlaying);
        foreach(GameObject enemy in enemies){
            if(enemy != null){
                enemy.GetComponent<PatrolPointsController>().enabled = isPlaying;
                enemy.GetComponent<Animator>().enabled = isPlaying;
                PausingAudioSources(enemy, isPlaying);
            }
        } 
        foreach(GameObject pickup in pickups){
            if(pickup != null)
                pickup.GetComponent<PulsateAnimation>().enabled = isPlaying;
        }
        foreach(GameObject trap in traps){
            if(trap != null)
                trap.GetComponent<Animator>().enabled = isPlaying;
        }
    }

    private void PausingAudioSources(GameObject obj, bool isPlaying){
        AudioSource[] audioSources = obj.GetComponents<AudioSource>();
        foreach(AudioSource audioSource in audioSources){
            if(isPlaying){
                audioSource.UnPause();
            } else {
                audioSource.Pause();
            }
        }
    }

    public void ChangeSliderMusic(float volume){
        musicMixer.SetFloat("musicVolume", volume);
    }

    public void ChangeSliderSounds(float volume){
        soundsMixer.SetFloat("soundsVolume", volume);
    }
    public void ContinueButton(){
        pauseMenuPanel.SetActive(false);
        mainAudioSource.UnPause();
        isPlaying = true;
        SetIsPlay(isPlaying);
        float soundsVol, musicVol;
        soundsMixer.GetFloat("soundsVolume", out soundsVol);
        musicMixer.GetFloat("musicVolume", out musicVol);

        SaveParams(soundsVol, musicVol);
    }

    private void SaveParams(float soundsVol, float musicVol){
        PlayerPrefs.SetFloat("soundsVol", soundsVol);
        PlayerPrefs.SetFloat("musicVol", musicVol);

        PlayerPrefs.Save();
    }

    private void LoadParams(){
        if(PlayerPrefs.HasKey("soundsVol")){
            soundsMixer.SetFloat("soundsVolume", PlayerPrefs.GetFloat("soundsVol"));
        } else {
            soundsMixer.SetFloat("soundsVolume", 0);
        }

        if(PlayerPrefs.HasKey("musicVol")){
            musicMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVol"));
        } else {
            musicMixer.SetFloat("musicVolume", 0);
        }
    }

    public void ExitGame(){
        float soundsVol, musicVol;
        soundsMixer.GetFloat("soundsVolume", out soundsVol);
        musicMixer.GetFloat("musicVolume", out musicVol);

        SaveParams(soundsVol, musicVol);
        Application.Quit();
    }
}