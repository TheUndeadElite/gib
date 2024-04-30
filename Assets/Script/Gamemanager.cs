using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gameIsPaused;

    [SerializeField]
    GameObject Pausemenu;
    [SerializeField]
    GameObject Resumebutton;
    

    [SerializeField] GameObject settingsBox;
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private TextMeshProUGUI ValueText;
    [SerializeField] private AudioMixMode MixMode;

    [SerializeField] private Slider volumeSlider;
    void Start()
    {
        volumeSlider.value = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {

            Pause();


        }
    }

    public void Pause()
    {
        if (!gameIsPaused)
        {
            gameIsPaused = true;
            Debug.Log("Game pause!");
            Pausemenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            gameIsPaused = false;
            Debug.Log("Game UNpause!");
          
            Pausemenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }

    public void Setting()
    {
        if(settingsBox.activeSelf)
        {
            settingsBox.SetActive(false);
        }   else
        {
            settingsBox.SetActive(true);
        }
       
    }
    public enum AudioMixMode
    {
        LinearAudioSourceVolume,
        LinearMixerVolume,
        LogrithmicMixerVolume
    }
    public void OnChangeSlider(float Value)
    {
        ValueText.SetText($"{Value.ToString("N4")}");

        switch (MixMode)
        {
            case AudioMixMode.LinearAudioSourceVolume:
                // Convert the slider value from 0-100 range to 0-1 range
                float linearVolume = Value / 100f;
                AudioSource.volume = linearVolume;
                break;
            case AudioMixMode.LinearMixerVolume:
                // Convert the slider value from 0-100 range to -80-20 range
                float mixerVolumeLinear = -80 + Value;
                Mixer.SetFloat("Volume", mixerVolumeLinear);
                break;
            case AudioMixMode.LogrithmicMixerVolume:
                // Convert the slider value from 0-100 range to -80-20 range
                float mixerVolumeLog = Mathf.Log10(Value / 100f) * 20;
                Mixer.SetFloat("Volume", mixerVolumeLog);
                break;
        }

        // Find the audio source component in the canvas
        AudioSource canvasAudioSource = FindObjectOfType<AudioSource>();

        // Mute the audio source if the slider value is close to 0
        if (Mathf.Approximately(Value, 0f) && MixMode == AudioMixMode.LinearAudioSourceVolume && canvasAudioSource != null)
        {
            canvasAudioSource.mute = true;
        }
        else
        {
            // Unmute the audio source
            canvasAudioSource.mute = false;
        }
    }



}
