using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop
{
    public int CropId { get; set; }
    public CardColor Color { get; set; }
    public Plant Plant { get; set; }
    public List<BoardSlot> Slots { get; set; } = new();
    public List<BoardSlot> UngrownSlots { get; set; } = new();
}
