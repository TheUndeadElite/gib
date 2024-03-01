using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
   public void Interact(PlayerController player)
    {
      player.DiglogueUI.showDialogue(dialogueObject);
    }
}
