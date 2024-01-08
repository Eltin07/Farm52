using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CropManager : MonoBehaviour
{
    public Board Board;
    public Crop LastPlayedCrop;
    public List<Crop> Crops = new();

    // void Awake() => Card.OnCardPlayed += GroupCrops;
    // void OnDestroy() => Card.OnCardPlayed -= GroupCrops;

    void Start()
    {
        Board = gameObject.GetComponentInParent<Board>();
    }  

    #region Game Functions
    public void HarvestCrop(BoardSlot slot)
    {
        Crop crop = Crops.Where(x => x.Slots.Contains(slot)).FirstOrDefault();
        GameManager.Instance.ScoreManager.AddHarvestScore(crop);
        List<BoardSlot> harvestedSlots = new();

        foreach (var cropSlot in crop.Slots)
        {
            if (!crop.UngrownSlots.Contains(cropSlot))
            {
                Destroy(cropSlot.gameObject.transform.GetChild(0).gameObject);
                cropSlot.Plant = null;
                harvestedSlots.Add(cropSlot);
            }
        }

        foreach(var cropSlot in harvestedSlots)
        {
            crop.Slots.Remove(cropSlot);
        }

        if (crop.Slots.Count == 0)
        {
            Crops.Remove(crop);
        }
    }

    public void PlantCardShape(List<BoardSlot> slots, Plant plant)
    {
        foreach(var slot in slots)
        {
            slot.FillSlot(plant);
        }
        Crop plantedCrop = NewCrop(slots, plant);

        // List<Crop> mergeableCrops = GetMergeableCrops(plantedCrop);

        // if (mergeableCrops.Count > 1)
        // {
        //     plantedCrop = MergeCrops(mergeableCrops);
        // }

        Crops.Add(plantedCrop);
        LastPlayedCrop = plantedCrop;
    }  
    
    // public void ManageCrops()
    // {
    //     foreach (var crop in Crops)
    //     {
    //         List<Crop> mergeableCrops = GetMergeableCrops(crop);

    //         if (mergeableCrops.Count > 1)
    //         {
    //             MergeCrops(mergeableCrops);
    //         }
    //     }
    // }
    #endregion

    #region Crop Functions
    public Crop NewCrop(List<BoardSlot> slots, Plant plant)
    {
        Crop newCrop = new Crop();
        newCrop.CropId = Crops.Count == 0 ? 0 : Crops.Max(x => x.CropId) + 1;
        newCrop.Plant = plant;
        newCrop.Slots.AddRange(slots);
        newCrop.UngrownSlots.AddRange(slots);

        return newCrop;
    }

    public void GroupCrops()
    {
        List<Crop> newCrops = new();
        bool cropsChecked = false;

        foreach(var crop in Crops)
        {
            List<Crop> mergeableCrops = GetMergeableCrops(crop);

            if (mergeableCrops.Count > 1)
            {
                MergeCrops(mergeableCrops);
                GroupCrops();
                break;
            }
        }
    }

    public void MergeCrops(List<Crop> cropsToMerge)
    {
        Crop mergedCrop = cropsToMerge[0];
        List<BoardSlot> slots = new();
        List<BoardSlot> ungrownSlots = new();

        foreach(var crop in cropsToMerge)
        {
            slots.AddRange(crop.Slots);
            ungrownSlots.AddRange(crop.UngrownSlots);
            Crops.Remove(crop);
        }

        mergedCrop.Slots = slots;
        mergedCrop.UngrownSlots = ungrownSlots;
        Crops.Add(mergedCrop);
    }

    //Returns list of all mergeable crops including crop that called the function
    public List<Crop> GetMergeableCrops(Crop crop)
    {
        List<Crop> cropsToMerge = new();
        cropsToMerge.Add(crop);

        List<BoardSlot> surroundingSlots = GetSurroundingSlots(crop);

        foreach(var slot in surroundingSlots)
        {
            if (slot.Plant == crop.Plant)
            {
                Crop mergeableCrop = Crops.Where(crop => crop.Slots.Contains(slot)).FirstOrDefault();
                cropsToMerge.Add(mergeableCrop);
            }
        }
        cropsToMerge = cropsToMerge.Distinct().ToList();

        return cropsToMerge;
    }

    public List<BoardSlot> GetSurroundingSlots(Crop crop)
    {
        List<BoardSlot> surroundingSlots = new();

        foreach(var slot in crop.Slots)
        {
            surroundingSlots.AddRange(slot.surroundingSlots);
        }

        surroundingSlots = surroundingSlots.Distinct().ToList();
        
        foreach(var slot in crop.Slots)
        {
            surroundingSlots.Remove(slot);
        }

        return surroundingSlots;
    }
    
    public void ExpandCrop(Crop crop)
    {
        List<BoardSlot> surroundingSlots = GetSurroundingSlots(crop);
        BoardSlot slot = surroundingSlots[Random.Range(0, surroundingSlots.Count)];

        while(slot.gameObject.transform.childCount > 0)
        {
            slot = surroundingSlots[Random.Range(0, surroundingSlots.Count)];
        }

        slot.FillSlot(crop.Plant);
        crop.Slots.Add(slot);

        // List<Crop> mergeableCrops = GetMergeableCrops(crop);
        // if (mergeableCrops.Count > 0)
        // {
        //     MergeCrops(mergeableCrops);
        // }

    }
    #endregion
}
