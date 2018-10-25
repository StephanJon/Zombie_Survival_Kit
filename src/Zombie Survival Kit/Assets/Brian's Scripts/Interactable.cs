using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable: Is a Class that will be used to distinguish gameObjects that can be
/// interacted by the player.
/// Interactable inherits the MonoBehaviour Class
/// </summary>
public class Interactable : MonoBehaviour {
    
    /* The minimum distance the player must be with the Interactable GameObject 
     * to interact with, and to keep the interaction with the GameObject.
     */
    public float radius;
    /* Shows if the the Interactable GameObject is being focused on.
     */
    public bool isFocus = false;
    /* Shows what is interacting wtih the Interactable GameObject.
     */
    public Transform player;
    /* Shows if the Interactable GameObject has been interacted with.
     */
    public bool hasInteracted = false;

    /// <summary>
    /// Interact(): Is a virtual void method that is re-programmable in all other
    /// classes that inherit the Interactable class.
    /// </summary>
    public virtual void Interact()
    {
        //This method is meant to be overwritten
        //Debug.Log("Interacting with " + transform.name);
    }

    /// <summary>
    /// Update(): Is a void method that is called once per frame
    /// </summary>
    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }

    }

    /// <summary>
    /// onFocused(Transform playerTransform): Is a void method that is used when 
    /// the player interacts with an Interactable GameObject and shows that the 
    /// one interacting with the GameObject is the player, and that the GameObject
    /// has been interacted with and is being focused on.
    /// </summary>
    /// <param name="playerTransform">The player object</param>
    public void onFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    /// <summary>
    /// onDefocused(): Is a void method that is used when the player removes 
    /// focus with an Interactable GameObject and shows that the no one is
    /// interacting with the GameObject, and that the GameObject is not being
    /// interacted with and is not being focused on.
    /// </summary>
    public void onDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    /// <summary>
    /// Shows the radius of the Interactable GameObject, revealing the minimum
    /// distance a player needs to be with the GameObject before interacting with it,
    /// and to keep interacting with the the GameObject.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
