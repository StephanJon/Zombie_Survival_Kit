using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth { get; private set; }

    public Stat dmg;
    public Stat armour;

    void Awake()
    {
        curHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            TakeDamage(10);
    }

    public void TakeDamage(int damage)
    {
        if (damage > armour.GetValue())
            damage -= armour.GetValue();
        else
            damage = 0;

        if (curHealth > damage)
            curHealth -= damage;
        else
            curHealth = 0;

        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (curHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        //Die, this is meant to be overwritten
        Debug.Log(transform.name + " has died.");

    }

}
