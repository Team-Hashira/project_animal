using UnityEngine;

public class SuppliesManager
{
    public int food { get; set; }
    public int ore { get; set; }

    public void AddFood(int figure)
    {
        food += figure;
    }

    public void LostFood(int figure)
    {
        food -= figure;
    }

    public void AddOre(int figure)
    {
        ore += figure;
    }

    public void LostOre(int figure)
    {
        ore -= figure;
    }

    public int SetFood()
    {
        return food;
    }

    public int SetOre() 
    { 
       return ore; 
    }
}
