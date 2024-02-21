using System.Collections;
using UnityEngine;
using TMPro;
using System.Globalization;

public class digaLogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private GameObject dialogueBox;

    private TypewriterEffect typewriterEffect;

    private void Start()
    {
      typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();
        showDialogue(testDialogue);
    }

    public void showDialogue(DialogueObject dialogueObject)
    {
     StartCoroutine(stepThruoghDialogue(dialogueObject));
      dialogueBox.SetActive(true);
    }
    
    private IEnumerator stepThruoghDialogue(DialogueObject dialogueObject)
    {
       

        foreach ( string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }

        CloseDialogueBox();
    }


     private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text= string.Empty;
    }


}
