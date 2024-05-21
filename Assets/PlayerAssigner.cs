using UnityEngine;

public class PlayerAssigner : MonoBehaviour
{
    void Start()
    {
        SnokenController snokenController = GetComponent<SnokenController>();
        if (snokenController != null && snokenController.player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Knight");
            if (playerObject != null)
            {
                snokenController.player = playerObject.transform;
                Debug.Log("Player assigned in script.");
            }
            else
            {
                Debug.LogError("No GameObject with tag 'Player' found.");
            }
        }
        else if (snokenController == null)
        {
            Debug.LogError("SnokenController not found on this GameObject.");
        }
        else
        {
            Debug.Log("Player already assigned in inspector.");
        }
    }
}
