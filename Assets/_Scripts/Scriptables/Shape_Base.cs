using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Obstacle")]
public class Shape_Base : ScriptableObject
{
    public int width;
    public int height;
    public List<Vector2> SquarePositions;
}
