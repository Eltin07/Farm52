using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public int CropId { get; set; }
    public CardColor Color { get; set; }
    public Plant Plant { get; set; }
    public List<int> SlotIds { get; set; } = new();
    public int GrowthLevel { get; set; }
}
