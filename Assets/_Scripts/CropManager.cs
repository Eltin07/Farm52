using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CropManager : MonoBehaviour
{
    public Board Board;
    public GameObject CropObject;
    public List<Crop> Crops = new();

    void Start()
    {
        Board = gameObject.GetComponentInParent<Board>();
    }  

    public void PlantCardShape(List<BoardSlot> slots, Plant plant)
    {
        foreach(var slot in slots)
        {
            slot.FillSlot(plant);
        }
        //NewCrop(shapeSlotIds, plant);
    }  

    // public void NewCrop(List<int> slots, Plant plant)
    // {
    //     Crop newCrop = new Crop();
    //     newCrop.CropId = Crops.Count == 0 ? 0 : Crops.Max(x => x.CropId) + 1;
    //     newCrop.Plant = plant;
    //     newCrop.SlotIds.AddRange(slots);

    //     Crops.Add(newCrop);
    // }

}
