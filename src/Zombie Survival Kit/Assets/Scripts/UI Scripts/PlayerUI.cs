using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PlayerUI: A class to manage changes in the player's user interface
/// </summary>
public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    RectTransform hpBarFill; //The amount of the health bar to fill red

    [SerializeField]
    Text ammoLeft; //Text to display ammo left in the clip

    //GameObject gunObject; //Reference to the player's gun and stats
    private Gun gun;
    private PlayerStats stats;

    /// <summary>
    /// SetStats: Initializes the player's stats and gun component so the UI can react accordingly
    /// </summary>
    /// <param name="playerStats">The stats of the player</param>
    public void SetStats(PlayerStats playerStats)
    {
        stats = playerStats;
        //gun = gunObject.GetComponent<Gun>();
    }

    private void Start()
    {
        SetStats(PlayerStats.instance);
    }

    /// <summary>
    /// Update: Is a void method that is called once per frame
    /// </summary>
    private void Update()
    {
        GameObject gun1 = GameObject.FindGameObjectWithTag("Gun");
        if (gun1 != null)
        {
            gun = gun1.GetComponent<Gun>();
            SetAmmoAmount(gun.getAmmoInClip().ToString());
        }
        else
        {
            SetAmmoAmount("-");
        }

        //Keep on updating the amount health and ammo to display
        SetHPFill(stats.curHealth);

    }

    /// <summary>
    /// SetHPFill: A method to fill up the health bar according the the player health remaining
    /// </summary>
    /// <param name="amount">The amount of health left</param>
    private void SetHPFill(float amount)
    {
        //Since health is out of a 100, it needs to be on a divided to a scale from 0 to 1
        hpBarFill.localScale = new Vector3(1f, amount/100f, 1f);
    }

    /// <summary>
    /// SetAmmoAmount: A method to modify the ammo UI acording to the ammo left in the clip
    /// </summary>
    /// <param name="amount">The amount of ammo left</param>
    private void SetAmmoAmount(string amount)
    {
        //Update the text
        ammoLeft.text = amount;
    }
}
