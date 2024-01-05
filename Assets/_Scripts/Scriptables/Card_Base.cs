using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Obstacle")]
public class Card_Base : ScriptableObject
{
    public Type Type;
    public CardColor CardColor;
    public Color32 HexColor;
    public Image CardImage;
    public Plant Plant;
}

[Serializable]
public enum Type {
    Plant = 0,
    Utility = 1
}
public enum CardColor {
    None,
    Yellow,
    Blue,
    Red,
    Rusty
}