using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Gun: A class used to define Gun behavior and define values required for Gun operation
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{

    [SerializeField] int ammoPerClip = 7; //max capacity for a clip
    int ammoInClip;  //the number of bullets in the current clip, if this reaches 0 a reload is needed

    [SerializeField] GameObject bullet; //reference to a bullet gameObject

    [SerializeField] AudioClip gunShotSound; //sound clip for gun shot

    [SerializeField] AudioClip reloadSound; //sound clip for reloading

    [SerializeField] float bulletSpeed = 100f; //speed of bullet

    [SerializeField] float fireRate = 0.8f; //rate of fire for weapon

    [SerializeField] float reloadRate = 2f; //reload speed for weapon

    [SerializeField] GameObject cam; //reference to MainCamera object

    Transform startPoint;//start point for bullet

    float startTime; //used to create a clock to keep track of time

    private AudioSource reloadSource;

    //public bool bulletHasBeenFired; //used for testing

    ItemStore rangedItem;

    GameObject playerCanvasUI;

    bool isInventoryActive;

    /*
    /// <summary>
    /// Contruct: Used to construct an Object of type Gun, used for unit testing purposes
    /// </summary>
    /// <param name="ammoinclip"></param>
    /// <param name="ammoperclip"></param>
    /// <param name="bulletobject"></param>
    /// <param name="bulletspeed"></param>
    /// <param name="firerate"></param>
    /// <param name="reloadrate"></param>
    public void Construct(int ammoinClip, int ammoperclip, GameObject bulletobject, GameObject camera, float bulletspeed, float firerate, float reloadrate)
    {
        cam = camera;
        ammoInClip = ammoinClip;
        ammoPerClip = ammoperclip;
        bullet = bulletobject;
        bulletSpeed = bulletspeed;
        fireRate = firerate;
        reloadRate = reloadrate;
    }
    */

    /// <summary>  
    ///  Start: This method runs at the start of the game and initialized the various variables required for the level.  
    /// </summary>  
    void Start()
    {
        startTime = Time.time;//start clock

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        startPoint = cam.transform;

        ammoInClip = ammoPerClip; //loads the weapon

        reloadSource = GetComponent<AudioSource>(); //assigns AudioSource to check status of reload sound

        rangedItem = GetComponent<ItemStore>();

        playerCanvasUI = GameObject.FindGameObjectWithTag("InventoryUI");
        var inventoryCanvas = playerCanvasUI.GetComponent<Canvas>();
        isInventoryActive = inventoryCanvas.enabled;
    }
    /// <summary>  
    ///  Update: This method is called once per frame.  
    /// </summary>  
	void Update()
    {
        var inventoryCanvas = playerCanvasUI.GetComponent<Canvas>();
        isInventoryActive = inventoryCanvas.enabled;

        float elapsedTime = Time.time - startTime; //current time - time since last shot
        if (Input.GetKeyDown(KeyCode.Mouse0) && !reloadSource.isPlaying && elapsedTime >= fireRate && isInventoryActive == false) //when left mouse button is clicked, and time passed since last shot is more than fire rate
        {
            shoot(); //shoot a bullet
            elapsedTime = 0f; //reset time since last shot to 0
            startTime = Time.time; //start time set to time of shot
        }
        else if (Input.GetKeyDown(KeyCode.R)) //if the 'R' button is pressed
        {
            reload(); //reload the gun
        }
    }

    /// <summary>  
    ///  shoot: This method shoots a bullet towards a target, and updates the number of bullets in the clip. If the clip is empty, this method reloads the weapon.  
    ///  It also instantiates a temporary "muzzle flash" at the end of the gun barrell
    /// </summary> 
    public void shoot()
    {
        if (ammoInClip > 0)
        {
            createBullet();
            //GetComponent<Animator>().SetTrigger("Fire");
            ammoInClip--;
           // bulletHasBeenFired = true;
        }
        else
        {
            reload();
        }
    }

    /// <summary>  
    ///  reload: This method reloads the weapon by restoring the number of bullets in the clip to full capacity.  
    /// </summary>  
    public void reload()
    {
        ammoInClip = ammoPerClip; //loads the weapon
        playReloadSound();
    }

    /// <summary>  
    ///  createBullet: This method instantiates a bullet at the end of the gun barrel and sends it forward with respect to the starting position.  
    /// </summary>  
    public void createBullet()
    {
        Instantiate(bullet, startPoint.position, startPoint.rotation).GetComponent<Rigidbody>().AddForce(startPoint.forward * bulletSpeed);
        playShotSound();
    }

    /// <summary>
    /// getAmmoInClip: An int method used for other classes to access the amount of ammo left in the clip
    /// </summary>
    /// <returns>The amount of ammo left in the clip</returns>
    public int getAmmoInClip()
    {
        return ammoInClip;
    }

    /// <summary>
    /// playShotSound: A private void method used to play the gun shot sound
    /// </summary>
    private void playShotSound()
    {
        AudioSource.PlayClipAtPoint(gunShotSound, cam.transform.position); //fire and forget sound implementation
    }

    /// <summary>
    /// playReloadSound: A private void method used to play the reload sound
    /// </summary>
    private void playReloadSound()
    {
        //the status of this sound effect can be checked
        reloadSource.clip = reloadSound;
        reloadSource.Play();
    }

}
