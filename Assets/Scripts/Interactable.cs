using UnityEngine;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

[RequireComponent(typeof(ColorOnHover))]
public class Interactable : MonoBehaviour
{

    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;   // Is this interactable currently being focused?
    Transform player;       // Reference to the player transform

    bool hasInteracted = false; // Have we already interacted with the object?

    void Update()
    {
        if (isFocus && !hasInteracted)    // If currently being focused
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            // If we haven't already interacted and the player is close enough
            if (distance <= radius)
            {
                // Interact with the object
                Interact();
                hasInteracted = true;
            }
        }
    }

    // Called when the object starts being focused
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        hasInteracted = false;
        player = playerTransform;
    }

    // Called when the object is no longer focused
    public void OnDefocused()
    {
        isFocus = false;
        hasInteracted = false;
        player = null;
    }

    // This method is meant to be overwritten
    public virtual void Interact()
    {
        Debug.Log("Picking up " + transform.name);
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = this.transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}