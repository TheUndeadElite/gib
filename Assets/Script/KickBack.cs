using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class KickBack : MonoBehaviour
{



   public void KickBackToMainMenu()
    {
        Debug.Log("This happens at the end of the credits screen");
        SceneManager.LoadScene(0);
    }
}
