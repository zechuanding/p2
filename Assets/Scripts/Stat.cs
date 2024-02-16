using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stat
{
    int savedMaxVal;
    int val;
    int maxVal;

    // Constructor
    public Stat(int n)
    {
        savedMaxVal = n;
        val = n;
        maxVal = n;
    }
    public int Get() { return val; }
    public void Add(int amount)
    {
        val += amount;
        if (val > maxVal) val = maxVal;
        else if (val < 0) val = 0;
    }

    public void Maximize()
    {
        val = maxVal;
    }

    public void Save()
    {
        savedMaxVal = maxVal;
    }
    public void Load()
    {
        maxVal = savedMaxVal;
        val = maxVal;
    }
}
