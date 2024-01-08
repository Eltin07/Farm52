using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlantCropHandler : MonoBehaviour
{
    private CropManager CropManager;

    void Awake() => Card.OnCardPlayed += HandleCrops;
    void OnDestroy() => Card.OnCardPlayed -= HandleCrops;

    void Start()
    {
        CropManager = gameObject.GetComponent<CropManager>();
    }

    private void HandleCrops()
    {
        foreach (var crop in CropManager.Crops)
        {
            GrowCrop(crop);
            PlantType type = crop.Plant.PlantType;

            switch (type)
            {
                case PlantType.Blue:
                    HandleWaterCrop(crop);
                    break;
                default:
                    break;
            }
        }

        CropManager.GroupCrops();
    }

    private void GrowCrop(Crop crop)
    {
        List<BoardSlot> SlotsToGrow = new();

        foreach(var slot in crop.UngrownSlots)
        {
            if (slot.TurnsTillGrowth == 0 && slot.GrowthLevel < crop.Plant.GrowthStages)
            {
                slot.GrowthLevel++;
                slot.SlotFill.GetComponent<Image>().sprite = crop.Plant.GrowthImages[slot.GrowthLevel];
                SlotsToGrow.Add(slot);
            }
            else
            {
                slot.TurnsTillGrowth--;
            }
        }

        foreach(var slot in SlotsToGrow)
        {
            crop.UngrownSlots.Remove(slot);
        }
    }

    private void HandleWaterCrop(Crop crop)
    {
        if (crop != CropManager.LastPlayedCrop)
            CropManager.ExpandCrop(crop);
    }
}
