using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardSlot : MonoBehaviour
{
    #region Fields
    public int SlotId;
    public Color32 SlotHexColor;
    public CardColor SlotColor;
    private GameObject SlotFill;
    private Board Board;
    public Vector2 GridCoords;

    public Plant Planted;

    public List<int> adjacentSlots = new();



    #endregion

    void Start()
    {
        Board = gameObject.GetComponentInParent<Board>();
        SlotId = transform.GetSiblingIndex();
    }


    private void GetAdjacentSlots()
    {
        float x = GridCoords.x;
        float y = GridCoords.y;

        List<Vector2> slots = new();
        slots.Add(new Vector2(x - 1, y));
        slots.Add(new Vector2(x + 1, y));
        slots.Add(new Vector2(x, y - 1));
        slots.Add(new Vector2(x, y + 1));
        
        foreach (var slot in slots)
        {
            bool badslot = false;
            if (slot.x < 0 || slot.x > Board.gridWidth - 1)
                badslot = true;
            if (slot.y < 0 || slot.y > Board.gridHeight - 1)
                badslot = true;
            
            if(!badslot)
            {
                adjacentSlots.Add(Board.grid[(int)slot.x, (int)slot.y]);
            }
        }
    }

    private void CheckSurrounding()
    {
        List<int> matchingSlotIds = new();

        foreach(var slotid in adjacentSlots)
        {
            if(Board.Slots[slotid].SlotColor == SlotColor)
            {
                Debug.Log("adjacent same color");
                matchingSlotIds.Add(slotid);
            }
        }

        // if(matchingSlotIds.Count > 0)
        // {
        //     Board.GroupManager.AddToGroup(this, matchingSlotIds);
        // }
        // else
        // {
        //     Board.GroupManager.CreateGroup(this);
        // }

    }

    private Transform GetOffsetSlot()
    {
        Transform offsetSlot;

        int offsetId = Board.grid[(int)(GridCoords.x), (int)(GridCoords.y - 1f)];
    
        offsetSlot = transform.parent.transform.GetChild(offsetId).transform;

        return offsetSlot;
    }


    public void FillSlot(Plant plant)
    {
        GetAdjacentSlots();
        SlotFill = Instantiate(Board.SlotFillBase, this.transform);
        SlotFill.GetComponent<Image>().color = plant.Color; 
        Planted = plant;
    }


}


    // public void OnDrop(PointerEventData eventData)
    // {

    //     GameObject dropped = eventData.pointerDrag;
    //     Transform DropSlot = GetOffsetSlot();

    //     if (dropped.tag == "Card" && DropSlot.childCount == 0)
    //     {
    //         //Get components of dropped card
    //         DraggableCard draggableCard = dropped.GetComponent<DraggableCard>();
    //         Card droppedCard = dropped.GetComponent<Card>();

    //         //Create new plant on dropped tile
    //         SlotFill = Instantiate(Board.SlotFillBase, DropSlot);
    //         SlotFill.GetComponent<Image>().color = droppedCard.color;

    //         SlotHexColor = droppedCard.color;
    //         SlotColor = droppedCard.card.CardColor;

    //         //Setting new parent destroys gameobject
    //         draggableCard.parentAfterDrag = transform;

    //         if (adjacentSlots.Count == 0)
    //         {
    //             GetAdjacentSlots();
    //         }
    //         CheckSurrounding();
    //     }

    //     if (dropped.tag == "HarvestTool" && DropSlot.childCount > 0)
    //     {
    //         BoardSlot slot = DropSlot.GetComponent<BoardSlot>();
    //         Board.GroupManager.HarvestGroup(slot);
    //     }

    //     if (dropped.tag == "Shape")
    //     {
    //         Shape_Base shape = dropped.GetComponent<Shape>().ShapeSpecs;

    //         DropShape(shape);
    //     }
    // }
