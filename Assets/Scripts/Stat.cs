using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stat
{
    int initialMaxVal;
    int val;
    int maxVal;
    int savedVal;

    // Constructor
    public Stat(int n)
    {
        initialMaxVal = n;
        val = n;
        maxVal = n;
    }
    public int Get() { return val; }
    public int GetMax() { return maxVal; }
    public void Add(int amount)
    {
        val += amount;
        if (val > maxVal) val = maxVal;
        else if (val < 0) val = 0;
    }

    public void AddMax(int amount)
    {
        maxVal += amount;
    }

    public void Maximize()
    {
        val = maxVal;
    }

    public void Save()
    {
        savedVal = val;
    }
    public void Load()
    {
        val = savedVal;
    }
}
