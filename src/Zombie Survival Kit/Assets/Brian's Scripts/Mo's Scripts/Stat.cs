using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    int initialValue;

    private List<int> statChanges = new List<int>();

    public int GetValue()
    {
        int finalValue = initialValue;
        statChanges.ForEach(x => finalValue += x);

        return finalValue;
    }

    public void AddToStat(int stat)
    {
        if (stat != 0)
            statChanges.Add(stat);
    }

    public void RemoveFromStat(int stat)
    {
        if (stat != 0)
            statChanges.Remove(stat);
    }

}
