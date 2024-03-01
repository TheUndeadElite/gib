using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
   public void Interact(Playercontroller player)
    {
      player.DiglogueUI.showDialogue(dialogueObject);
    }
}
