using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingSO", menuName = "SO/Building/Building")]
public class BuildingSO : ScriptableObject
{
    public string buildingName;
    public Sprite sprite;
    public SerializedDictionary<EResourceType, int> recipe;
    public Building building;
}
