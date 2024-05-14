using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public TMP_Text dialogText;
    public string[] dialogues; // Array to hold multiple dialogues
    private int currentDialogIndex = 0; // Index to keep track of current dialogue
    private bool dialogActive = false; // Flag to indicate if dialogue is active
    public bool PlayerInRange;

    private Coroutine typeCoroutine; // Coroutine reference for typing effect

    bool hasReadSign = false;

    void Start()
    {
        // Check if dialogues array is empty
        if (dialogues.Length == 0)
        {
            Debug.LogError("No dialogues assigned to the sign.");
            return;
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && PlayerInRange)
        {
            hasReadSign = true;

            Debug.Log(dialogActive);

            if (dialogActive)
            {
                if (typeCoroutine != null)
                {   
                    StopCoroutine(typeCoroutine); // Stop typing coroutine if it's running
                }

                if (currentDialogIndex < dialogues.Length - 1)
                {
                    // If there are more dialogues to show, move to the next dialogue
                    currentDialogIndex++;
                    dialogText.text = ""; // Clear dialog text before displaying
                    typeCoroutine = StartCoroutine(TypeDialog(dialogues[currentDialogIndex])); // Start typing coroutine for next dialogue
                }
                else
                {
                    // If it's the last dialogue, close the dialog box and reset the dialog index
                    dialogBox.SetActive(false);
                    dialogActive = false;
                    currentDialogIndex = 0; // Reset dialog index to start over
                }
            }
            else
            {
                // If dialog is not active, load and display the first dialogue
                dialogText.text = ""; // Clear dialog text before displaying
                typeCoroutine = StartCoroutine(TypeDialog(dialogues[currentDialogIndex])); // Start typing coroutine for first dialogue
                dialogBox.SetActive(true); // Activate dialog box
                dialogActive = true; // Set dialog active flag to true
            }
        }
    }

    IEnumerator TypeDialog(string dialog)
    {
        foreach (char letter in dialog)
        {
            dialogText.text += letter; // Add letter to dialog text
            yield return new WaitForSeconds(0.02f); // Wait for a short time before typing next letter
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Knight"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Knight"))
        {
            PlayerInRange = false;
        }
    }

    public bool GetHasReadSign()
    {
        return hasReadSign;
    }
}