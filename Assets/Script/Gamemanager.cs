using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gameIsPaused;

    [SerializeField]
    GameObject Pausemenu;
    [SerializeField]
    GameObject Resumebutton;
    void Start()
    {
        
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
}
