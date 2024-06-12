using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSettings : MonoBehaviour {
    [SerializeField]
    public List<Resolution> resolutions = new List<Resolution>();
    [SerializeField]
    public TMP_Dropdown dropdown;
    void Start(){
        dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(dropdown); });
        Screen.SetResolution(1920, 1080, true);
    }

    void Update(){

    }

    public void DropdownValueChanged(TMP_Dropdown change){
        Screen.SetResolution(resolutions[change.value].width, resolutions[change.value].height, true);
    }

    [System.Serializable]
    public class Resolution {
        public int width;
        public int height;
    }
}
