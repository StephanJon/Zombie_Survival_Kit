using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stat: A class to manage a type of stats and its modifiers
/// </summary>
[System.Serializable]
public class Stat
{
    [SerializeField]
    int initialValue; //Initial value of the stat

    //List of modifiers from items to add to the stat
    private List<int> statChanges = new List<int>();

    /// <summary>
    /// GetValue: An integer method that adds all the stat changes onto the intial value
    /// </summary>
    /// <returns>The modified stat value</returns>
    public int GetValue()
    {
        //Add each of the modifiers in statChanges to the intial value and return this
        int finalValue = initialValue;
        statChanges.ForEach(x => finalValue += x);
        return finalValue;
    }

    /// <summary>
    /// AddToStat: A method to add a modifier to the statChanges list
    /// </summary>
    /// <param name="stat">The value of the stat modifier being added</param>
    public void AddToStat(int stat)
    {
        if (stat != 0)
            statChanges.Add(stat);
    }

    /// <summary>
    /// RemoveFromStat: A method to remove a modifier from the statChanges list
    /// </summary>
    /// <param name="stat">The value of the stat modifier being removed</param>
    public void RemoveFromStat(int stat)
    {
        if (stat != 0)
            statChanges.Remove(stat);
    }

    public List<int> GetStatChanges()
    {
        return statChanges;
    }

    public int GetInitialValue()
    {
        return initialValue;
    }
}
