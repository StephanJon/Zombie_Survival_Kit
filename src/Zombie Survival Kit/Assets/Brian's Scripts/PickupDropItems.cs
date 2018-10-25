using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PickupDropItems: Is a class that will be used too receieve user input. When the user provides the correct
/// input, the player will pick up the item. It is also a class used to drop items
/// </summary>
public class PickupDropItems : MonoBehaviour
{

    /* Creating the length of the Raycast
     */
    public float distanceToSee;

    /* Creating a public interactible variable that stores whatever the player had just focused on
     */
    public Interactable focus;

    /* Creating a public interactible variable
     */
    RaycastHit hit;

    /* Keeps a reference of the player
     */
    public GameObject player;

    /* Checks to see if the player is carrying anything
     */
    public bool carrying;

    /* The object the player is carrying
     */
    public GameObject carriedObject;

    /* Determines how far out from the player the object is being carried
     */
    public float carryingDistance;

    /* Determines the smoothness in which the object is carried and moved around
     */
    public float smooth;

    /// <summary>
    /// Start(): Is a void method used for initialization
    /// </summary>
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera");
    }
        
    /// <summary>
    /// Update(): Is a void method that is called once per frame
    /// </summary>
    void Update()
    {
        /* Draws to screen the length of the raycast
         */
        Debug.DrawRay(transform.position, this.transform.forward * distanceToSee, Color.magenta);

        /* Checks the player is carrying something. If it is, then move carriedObject. Else, check for pickup instruction.
         */
        if (carrying)
        {
            carry(carriedObject);
            dropCheck();
        }
        else
        {
            pickup();
        }

        AttackEnemy();

        /* Checks if the player is focused on an interactable and checks the distance from the interactable to the player
         * If the player is outside the radius of the interactable, RemoveFocus()
         */
        if (focus != null && 
            Vector3.Distance(player.transform.position, focus.transform.position) > focus.radius)
        {
            RemoveFocus();
        }
    }

    /// <summary>
    /// Carry(GameObject o): Is a void method used to carry a GameObject that is Pickupable in front of the player
    /// camera specified by the carryingDistance.
    /// </summary>
    /// <param name="o">The GameObject being carried</param>
    void carry(GameObject o)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            o.GetComponent<Rigidbody>().isKinematic = false;
            o.transform.position = o.transform.position;

        }
        o.GetComponent<Rigidbody>().isKinematic = true;
        o.transform.position = Vector3.Lerp(o.transform.position,
            player.transform.position + player.transform.forward * carryingDistance,
            Time.deltaTime * smooth);
    }

    /// <summary>
    /// dropCheck(): Is a void method that checks if the correct input to drop the GameObject being carried is
    /// inputted.
    /// </summary>
    void dropCheck()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dropObject();
        }
    }

    /// <summary>
    /// dropObject(): Is a void method used to drop the GameObject being carried.
    /// </summary>
    void dropObject()
    {
        RemoveFocus();
        carrying = false;
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        // Brian:   Displays message in Log
        Debug.Log("I dropped a " + carriedObject.gameObject.name);
        carriedObject = null;
        
    }

    /// <summary>
    /// pickup(): Is a void method used to raycast a GameObject when the appropriate input is given, and only 
    /// interacts with the GameObject object if it is an interactable, and only carries the object if it is 
    /// a Pickupable.
    /// </summary>
    void pickup()
    {
        /* Creating an interaction with interactiable objects in game when user 
         * presses down the "E" key on their keyboard.
         */
        if (Input.GetKeyDown(KeyCode.E))
        {
            /* If the ray hits, do something
             */
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distanceToSee))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                /* Checks of the game object is interactable
                 */
                if (interactable != null)
                {
                    SetFocus(interactable);
                                                        
                    Pickupable p = hit.collider.GetComponent<Pickupable>();
                    /* If the Object is interactable and pickupable, then the player 
                     * will carry the object
                     */
                    if (p != null)
                    {
                        carrying = true;
                        carriedObject = p.gameObject;
                        // Displays message in Log
                        Debug.Log("I picked up a " + hit.collider.gameObject.name);
                    }

                }

            }

        }
    }

    void AttackEnemy()
    {
        /* Creating an interaction with interactiable objects in game when user 
         * presses the LMB on their mouse
         */
        if (Input.GetMouseButtonDown(0))
        {
            /* If the ray hits, do something
             */
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distanceToSee))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                /* Checks of the game object is interactable
                 */
                if (interactable != null)
                    SetFocus(interactable);
            }

        }
    }



    /// <summary>
    /// SetFocus(Interactable newFocus): Creates a focus on the object that the player interactes with.
    /// </summary>
    /// <param name="newFocus">The new interactable that the player is focusing on</param>
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.onDefocused();
            focus = newFocus;
        }
        
        newFocus.onFocused(transform);
    }

    /// <summary>
    /// RemoveFocus(): Removes the focus that the player is focusing on.
    /// </summary>
    /// <param name="newFocus">The new interactable that the player is focusing on</param>
    void RemoveFocus()
    {
        if (focus != null)
            focus.onDefocused();
        focus = null;
    } 
    
}

