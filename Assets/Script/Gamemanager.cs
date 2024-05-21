using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gameIsPaused;
    public float audioVolume = 50f;
    public GameObject HideSettings;
    

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsBox;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private AudioMixMode mixMode;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] TMP_Text debugText;

    void Start()
    {
        audioVolume = PlayerPrefs.GetFloat("audioVolume");
        if (volumeSlider != null)
        {
            volumeSlider.value = audioVolume;
            PlayerPrefs.SetFloat("audioVolume", audioVolume);
            OnChangeSlider(audioVolume);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void MainMenuscene()
    {
        SceneManager.LoadScene("MainMenu");

        gameIsPaused = false;
        Debug.Log("Game UNpause!");

        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        if (!gameIsPaused)
        {
            gameIsPaused = true;
            Debug.Log("Game pause!");
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            gameIsPaused = false;
            Debug.Log("Game UNpause!");

            pauseMenu.SetActive(false);
            Time.timeScale = 1;


        }
    }

    //void subsrice to OnLoad...
    //
    //gameIsPaused = false;
    //        Debug.Log("Game UNpause!");

    //        pauseMenu.SetActive(false);
    //        Time.timeScale = 1;
    public void GetVolumeFromSlider()
    {
        audioVolume = volumeSlider.value;
        PlayerPrefs.SetFloat("audioVolume", audioVolume);

        OnChangeSlider(volumeSlider.value);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }

    public void Setting()
    {
        if (settingsBox.activeSelf)
        {
            settingsBox.SetActive(false);
        }
        else
        {
            settingsBox.SetActive(true);
        }

    }

    public enum AudioMixMode
    {
        LinearAudioSourceVolume,
        LinearMixerVolume,
        LogarithmicMixerVolume
    }

    public void OnChangeSlider(float value)
    {
        valueText.SetText($"{value.ToString("N4")}");

        switch (mixMode)
        {
            case AudioMixMode.LinearAudioSourceVolume:
                // Convert the slider value from 0-100 range to 0-1 range
                float linearVolume = value / 100f;
                audioSource.volume = linearVolume;
                break;
            case AudioMixMode.LinearMixerVolume:
                // Convert the slider value from 0-100 range to -80-20 range
                float mixerVolumeLinear = -80 + value;
                mixer.SetFloat("Volume", mixerVolumeLinear);
                break;
            case AudioMixMode.LogarithmicMixerVolume:
                // Convert the slider value from 0-100 range to -80-20 range
                float mixerVolumeLog = Mathf.Log10(value / 100f) * 20;
                mixer.SetFloat("Volume", mixerVolumeLog);
                break;
        }

        // Find the audio source component in the scene
        AudioSource canvasAudioSource = FindObjectOfType<AudioSource>();

        // Mute the audio source if the slider value is close to 0
        if (Mathf.Approximately(value, 0f) && mixMode == AudioMixMode.LinearAudioSourceVolume && canvasAudioSource != null)
        {
            canvasAudioSource.mute = true;
        }
        else
        {
            // Unmute the audio source
            canvasAudioSource.mute = false;
        }
    }

    public void HideSettingsCanvas()
    {
        if (HideSettings != null)
        {
            HideSettings.SetActive(false);
        }
    }
}
