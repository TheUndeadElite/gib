using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    // Method to change the scene to scene number 5
    public void ChangeToScene5()
    {
        SceneManager.LoadScene(5); // This will load the scene with index 5
    }
}
