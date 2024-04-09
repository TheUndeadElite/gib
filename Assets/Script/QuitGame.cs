using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the button click event
        quitButton.onClick.AddListener(QuitGameFunction);
    }

    // Function to quit the game
    void QuitGameFunction()
    {
        // Quit the application
        Application.Quit();
    }
}
