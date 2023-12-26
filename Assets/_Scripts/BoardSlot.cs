using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardSlot : MonoBehaviour, IDropHandler
{
    #region Fields
    public int SlotId;
    public CardColor SlotColor;
    private GameObject SlotFill;
    private Board Board;
    public Vector2 GridCoords;

    public List<int> adjacentSlots = new();
    #endregion

    void Start()
    {
        Board = gameObject.GetComponentInParent<Board>();
        SlotId = transform.GetSiblingIndex();

    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        if (dropped.tag == "Card" && transform.childCount == 0)
        {
            //Get components of dropped card
            DraggableCard draggableCard = dropped.GetComponent<DraggableCard>();
            Card droppedCard = dropped.GetComponent<Card>();

            //Get actual tile based on Offset
            Transform DropSlot = GetOffsetSlot();

            //Create new plant on dropped tile
            SlotFill = Instantiate(Board.SlotFillBase, DropSlot);
            SlotFill.GetComponent<Image>().color = droppedCard.color;
            SlotColor = droppedCard.card.CardColor;

            //Setting new parent destroys gameobject
            draggableCard.parentAfterDrag = transform;

            if (adjacentSlots.Count == 0)
            {
                GetAdjacentSlots();
            }
            CheckSurrounding();
        }

        if (dropped.tag == "HarvestTool" && transform.childCount > 0)
        {
            Board.GroupManager.HarvestGroup(this);
        }

        if (dropped.tag == "Shape")
        {
            Debug.Log("Shape dropped at: " + SlotId);
        }
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

        if(matchingSlotIds.Count > 0)
        {
            Board.GroupManager.AddToGroup(this, matchingSlotIds);
        }
        else
        {
            Board.GroupManager.CreateGroup(this);
        }

    }

    private Transform GetOffsetSlot()
    {
        Transform offsetSlot;

        int offsetId = Board.grid[(int)(GridCoords.x - 1f), (int)(GridCoords.y - 1f)];
        Debug.Log(offsetId);
    
        offsetSlot = transform.parent.transform.GetChild(offsetId).transform;

        return offsetSlot;
    }
}
