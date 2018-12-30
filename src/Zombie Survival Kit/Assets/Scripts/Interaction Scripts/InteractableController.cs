using System.Collections;
using UnityEngine;

/// <summary>
/// PickupDropItems: Is a class that will be used too receieve user input. When the user provides the correct
/// input, the player will pick up the item. It is also a class used to drop items
/// </summary>
public class InteractableController : MonoBehaviour
{

    /* Creating the length of the Raycast
     */
    [SerializeField] private float distanceToSee;

    /* Creating a public interactible variable that stores whatever the player had just focused on
     */
    private Interactable focus;

    /* Creating a public interactible variable
     */
    private RaycastHit hit;

    /* Keeps a reference of the player
     */
    private GameObject player;

    ///* Checks to see if the player is carrying anything
    // */
    //public bool carrying;

    ///* The object the player is carrying
    // */
    //public GameObject carriedObject;

    ///* Determines how far out from the player the object is being carried
    // */
    //public float carryingDistance;

    ///* Determines the smoothness in which the object is carried and moved around
    // */
    //public float smooth;

    /// <summary>
    /// Start: Is a void method used for initialization
    /// </summary>
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera");
    }

    /// <summary>
    /// Update: Is a void method that is called once per frame
    /// </summary>
    void Update()
    {
        /* Draws to screen the length of the raycast
         */
        Debug.DrawRay(player.transform.position, player.transform.forward * distanceToSee, Color.magenta);

        /* Creating an interaction with interactiable objects in game when user 
         * presses down the "E" key on their keyboard.
         */
        if (Input.GetKeyDown(KeyCode.E)) {
            pickup();
        }

        /* Creating an interaction with interactiable objects in game when user 
         * presses the LMB on their mouse
         */
        if (Input.GetMouseButtonDown(0) && !EquipmentManager.instance.IsGunEquipped())
        {
            AttackEnemy();
        }

        /* Checks if the player is focused on an interactable and checks the distance from 
         * the interactable to the player. If the player is outside the radius of the 
         * interactable, RemoveFocus().
         */
        if (focus != null &&
            Vector3.Distance(player.transform.position, focus.transform.position) > focus.radius)
        {
            RemoveFocus();
        }
    }

    ///// <summary>
    ///// Carry: Is a void method used to carry a GameObject that is Pickupable in front of the player
    ///// camera specified by the carryingDistance.
    ///// </summary>
    ///// <param name="o">The GameObject being carried</param>
    //void carry(GameObject o)
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        o.GetComponent<Rigidbody>().isKinematic = false;
    //        o.transform.position = o.transform.position;

    //    }
    //    o.GetComponent<Rigidbody>().isKinematic = true;
    //    o.transform.position = Vector3.Lerp(o.transform.position,
    //        player.transform.position + player.transform.forward * carryingDistance,
    //        Time.deltaTime * smooth);
    //}

    ///// <summary>
    ///// dropCheck: Is a void method that checks if the correct input to drop the GameObject being 
    ///// carried is inputted.
    ///// </summary>
    //void dropCheck()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        dropObject();
    //    }
    //}

    ///// <summary>
    ///// dropObject: Is a void method used to drop the GameObject being carried.
    ///// </summary>
    //void dropObject()
    //{
    //    RemoveFocus();
    //    carrying = false;
    //    carriedObject.GetComponent<Rigidbody>().isKinematic = false;
    //    // Brian:   Displays message in Log
    //    Debug.Log("I dropped a " + carriedObject.gameObject.name);
    //    carriedObject = null;

    //}

    /// <summary>
    /// pickup: Is a void method used to raycast a GameObject when the appropriate input is given, and only 
    /// interacts with the GameObject object if it is an interactable, and only carries the object if it is 
    /// a Pickupable.
    /// </summary>
    private void pickup()
    {
        /* If the ray hits, do something
            */
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, distanceToSee))
        {
            ItemStore interactable = hit.collider.GetComponent<ItemStore>();
            /* Checks of the game object is interactable
                */
            if (interactable != null)
            {
                SetFocus(interactable);

                //Pickupable p = hit.collider.GetComponent<Pickupable>();
                ///* If the Object is interactable and pickupable, then the player 
                // * will carry the object
                // */
                //if (p != null)
                //{
                //    carrying = true;
                //    carriedObject = p.gameObject;
                //    // Displays message in Log
                //    Debug.Log("I picked up a " + hit.collider.gameObject.name);
                //}

            }

        }
    }

    private void AttackEnemy()
    {
        /* If the ray hits, do something
            */
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, distanceToSee))
        {
            if (hit.collider.GetComponent<ItemStore>() == null)
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
    /// SetFocus: Creates a focus on the object that the player interactes with.
    /// </summary>
    /// <param name="newFocus">The new interactable that the player is focusing on</param>
    private void SetFocus(Interactable newFocus)
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
    /// RemoveFocus: Removes the focus that the player is focusing on.
    /// </summary>
    /// <param name="newFocus">The new interactable that the player is focusing on</param>
    private void RemoveFocus()
    {
        if (focus != null)
            focus.onDefocused();
        focus = null;
    }

}

