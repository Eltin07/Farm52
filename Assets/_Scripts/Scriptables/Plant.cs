using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Obstacle")]
public class Plant : ScriptableObject
{
    public Color32 Color;
    public PlantType PlantType;
    public int TurnsToGrow;
    public int GrowthStages;

    public List<Sprite> GrowthImages = new();
}

public enum PlantType
{
    None,
    Yellow,
    Blue,
    Red
}
