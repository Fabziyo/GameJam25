//using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    public Slider master;
    public Slider music;
    public Slider effects;
    public Slider atmo;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Audio
        master.onValueChanged.AddListener((newValue) =>
        {
            PlayerPrefs.SetFloat("MasterVolume", newValue);
            //RuntimeManager.GetVCA("vca:/Master").setVolume(master.value);
        });
        master.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        //RuntimeManager.GetVCA("vca:/Master").setVolume(master.value);
        //RuntimeManager.GetBus("vca:/Master").setVolume(master.value);
        
        music.onValueChanged.AddListener((newValue) =>
        {
            PlayerPrefs.SetFloat("MusicVolume", newValue);
           // RuntimeManager.GetVCA("vca:/Music").setVolume(music.value);
        });
        music.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
       // RuntimeManager.GetVCA("vca:/Music").setVolume(music.value);
        
        effects.onValueChanged.AddListener((newValue) =>
        {
            PlayerPrefs.SetFloat("EffectsVolume", newValue);
            //RuntimeManager.GetVCA("vca:/Effects").setVolume(effects.value);
            //RuntimeManager.GetVCA("vca:/Dialogue").setVolume(effects.value);
        });
        effects.value = PlayerPrefs.GetFloat("EffectsVolume", 0.5f);
        //RuntimeManager.GetVCA("vca:/Effects").setVolume(effects.value);
        //RuntimeManager.GetVCA("vca:/Dialogue").setVolume(effects.value);
        
        atmo.onValueChanged.AddListener((newValue) =>
        {
            PlayerPrefs.SetFloat("AtmoVolume", newValue);
            //RuntimeManager.GetVCA("vca:/Atmo").setVolume(atmo.value);
        });
        atmo.value = PlayerPrefs.GetFloat("AtmoVolume", 0.5f);
        //RuntimeManager.GetVCA("vca:/Atmo").setVolume(atmo.value);
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
