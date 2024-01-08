using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCrop : MonoBehaviour
{
    public Crop Crop;

    void Awake() => Card.OnCardPlayed += ExpandCrop;
    void OnDestroy() => Card.OnCardPlayed -= ExpandCrop;

    void Start()
    {
        Setup();
    }

    private void Setup()
    {

    }

    private void ExpandCrop()
    {
        List<BoardSlot> slotsToExpand = GameManager.Instance.CropManager.GetSurroundingSlots(Crop);

        BoardSlot slot = slotsToExpand[Random.Range(0, slotsToExpand.Count)];

        while(slot.gameObject.transform.childCount > 0)
        {
            slot = slotsToExpand[Random.Range(0, slotsToExpand.Count)];
        }

        slot.FillSlot(Crop.Plant);
    }
}
