using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class sign : MonoBehaviour
{
    public GameObject DialogueBox;
    public Text dialogueText;
    public string dialogue;
    public bool PlayerInRang;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.E) && PlayerInRang) 
        {
         if (DialogueBox.activeInHierarchy)
            {
                DialogueBox.SetActive(false);
            }
            else
            {
                DialogueBox.SetActive(true);
                dialogueText.text = dialogue;
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           PlayerInRang = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRang = false;
            DialogueBox.SetActive(false);
        }
    }


}
