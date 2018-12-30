using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ZombieStats: A class used to manage stats specific to the zombies, inherited from CharacterStats
/// </summary>
public class ZombieStats : CharacterStats
{
    //Animator to control death animation
    private Animator animator;

    //Delay and timer to allow animation to complete
    [SerializeField] float delay;
    private float timeAnimStarted;

    //Booleans to check if the zombie has died or dropped an item
    private bool isDead = false;
    private bool isDropped = false;

    //Array of droppable objects
    private GameObject[] drops;

    //To store the death location
    public Transform zombie;

    /// <summary>
    /// Start: Is a void method used for initialization
    /// </summary>
    private void Start()
    {
        //Initializes the animator reference and their initial health to max 
        animator = GetComponent<Animator>();
        curHealth = maxHealth;

        //Loads All the possible items that can be dropped by a zombie
        drops = new GameObject[10];
        drops[0] = Resources.Load<GameObject>("PrefabItems/HeadArmor");
        drops[1] = Resources.Load<GameObject>("PrefabItems/Axe");
        drops[2] = Resources.Load<GameObject>("PrefabItems/RangeWeapon");
        drops[3] = Resources.Load<GameObject>("PrefabItems/Watermelon");
        drops[4] = Resources.Load<GameObject>("PrefabItems/LegArmor");
        drops[5] = Resources.Load<GameObject>("PrefabItems/Apple");
        drops[6] = Resources.Load<GameObject>("PrefabItems/ChestArmor");
        drops[7] = Resources.Load<GameObject>("PrefabItems/FeetArmor");
        drops[8] = Resources.Load<GameObject>("PrefabItems/OffHand");
        drops[9] = Resources.Load<GameObject>("PrefabItems/Cloak");
    }

    /// <summary>
    /// Update: Is a void method that is called once per frame
    /// </summary>
    private void Update()
    {
        //If the delay has passed since the zombie's health has reached 0
        if (Time.time >= timeAnimStarted + delay && isDead == true)
        {
            //Mark the death location and destroy the zombie game object
            Vector3 deathLocation = zombie.transform.position;
            Destroy(gameObject);

            //Drops a random item
            System.Random r = new System.Random();
            int dropChoice = r.Next(0, 10);
            DropItem(deathLocation, dropChoice);
        }
    }

    /// <summary>
    /// Die: An overwritten Die method to control what happens when the zombie dies
    /// </summary>
    public override void Die()
    {
        base.Die();
        //zombie.GetComponent<Enemy>().enabled = false;

        //Let the update method know the zombie is dead, play the animation, and start the timer
        isDead = true;
        animator.SetBool("dead", true);
        timeAnimStarted = Time.time;
    }

    /// <summary>
    /// DropItem: A void method used to select an item to drop after an enemy has died
    /// </summary>
    /// <param name="deathLocation">The location of the dead enemy</param>
    /// <param name="dropChoice">Determines which item is dropped from the dead enemy</param>
    private void DropItem(Vector3 deathLocation, int dropChoice)
    {
        if (!isDropped) //So multiple items do not drop from the enemy
        {
            isDropped = true;

            switch (dropChoice)
            {
                case 0:
                    Debug.Log("Dropped HeadArmor");
                    GameObject HeadArmor = Instantiate(drops[dropChoice]) as GameObject;
                    HeadArmor.transform.position = deathLocation;
                    break;
                case 1:
                    Debug.Log("Dropped MeleeWeapon");
                    GameObject MeleeWeapon = Instantiate(drops[dropChoice]) as GameObject;
                    MeleeWeapon.transform.position = deathLocation;
                    break;
                case 2:
                    Debug.Log("Dropped RangeWeapon");
                    GameObject RangeWeapon = Instantiate(drops[dropChoice]) as GameObject;
                    RangeWeapon.transform.position = deathLocation;
                    break;
                case 3:
                    Debug.Log("Dropped Watermelon");
                    GameObject Watermelon = Instantiate(drops[dropChoice]) as GameObject;
                    Watermelon.transform.position = deathLocation;
                    break;
                case 4:
                    Debug.Log("Dropped LegArmor");
                    GameObject LegArmor = Instantiate(drops[dropChoice]) as GameObject;
                    LegArmor.transform.position = deathLocation;
                    break;
                case 5:
                    Debug.Log("Dropped Apple");
                    GameObject Apple = Instantiate(drops[dropChoice]) as GameObject;
                    Apple.transform.position = deathLocation;
                    break;
                case 6:
                    Debug.Log("Dropped ChestArmor");
                    GameObject ChestArmor = Instantiate(drops[dropChoice]) as GameObject;
                    ChestArmor.transform.position = deathLocation;
                    break;
                case 7:
                    Debug.Log("Dropped FeetArmor");
                    GameObject FeetArmor = Instantiate(drops[dropChoice]) as GameObject;
                    FeetArmor.transform.position = deathLocation;
                    break;
                case 8:
                    Debug.Log("Dropped OffHand");
                    GameObject OffHand = Instantiate(drops[dropChoice]) as GameObject;
                    OffHand.transform.position = deathLocation;
                    break;
                case 9:
                    Debug.Log("Dropped Cloak");
                    GameObject Cloak = Instantiate(drops[dropChoice]) as GameObject;
                    Cloak.transform.position = deathLocation;
                    break;
            }
        }
    }

    /// <summary>
    /// IsDead: A boolean method that allows other classes to know if the zombie has died
    /// </summary>
    /// <returns>true or false</returns>
    public bool IsDead()
    {
        return isDead;
    }
}
