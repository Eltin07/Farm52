using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Obstacle")]
public class Card_Base : ScriptableObject
{
    public Type Type;
    public Image CardImage;
}

[Serializable]
public enum Type {
    Plant = 0,
    Utility = 1
}
