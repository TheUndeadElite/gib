using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    
    public void CloseGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
