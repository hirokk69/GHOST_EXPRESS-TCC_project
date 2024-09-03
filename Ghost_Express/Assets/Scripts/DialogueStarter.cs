using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myDialogue;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (Input.GetKeyDown (KeyCode.F)) 
            {
                ConversationManager.Instance.StartConversation(myDialogue);
            }
        }
    }

}
