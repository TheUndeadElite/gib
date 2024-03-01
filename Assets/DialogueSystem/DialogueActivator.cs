using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.CompareTag("Player") && other.TryGetComponent(out PlayerController Player))
        {
            Player.interactabel = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController Player))
        {
           if (Player.interactabel is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                Player.interactabel = null;
            }
        }
    }

    public void Interact(PlayerController player)
    {
      player.DiglogueUI.showDialogue(dialogueObject);
    }
}
