using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    public GraphicRaycaster _raycaster;
    public PointerEventData _pointerEventData;
    public EventSystem _eventSystem;
    public Board board;
    public BoardSlot DroppedSlot;
    public DiscardPile _discardPile;
    public Color32 TestColor;

    void Start()
    {
        _raycaster = GetComponentInParent<GraphicRaycaster>();
        _eventSystem = GetComponentInParent<EventSystem>();

        board = gameObject.GetComponentInChildren<Board>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        _discardPile = null;
        DroppedSlot = null;
        bool WasOnGameBoard = false;

        _pointerEventData = new PointerEventData(_eventSystem);
       
        eventData.position = Input.mousePosition;
        GameObject dropped = eventData.pointerDrag;

        List<RaycastResult> RaycastResults = new List<RaycastResult>();
        _raycaster.Raycast(eventData, RaycastResults);


        foreach (var result in RaycastResults)
        {
            if (result.gameObject.tag == "BoardSlot")   
                DroppedSlot = result.gameObject.GetComponent<BoardSlot>();
            if (result.gameObject.tag == "Discard") 
                _discardPile = result.gameObject.GetComponent<DiscardPile>();
            if (result.gameObject.tag == "GameBoard")
                WasOnGameBoard = true;
        }
        string droppedTag = dropped.tag;

        switch(droppedTag)
        {
            case "Card":
                Card card = dropped.GetComponent<Card>();
                Shape shape = dropped.GetComponentInChildren<Shape>();

                if (WasOnGameBoard)
                    DropShape(shape, card);

                if (_discardPile != null)
                {
                    _discardPile.DiscardCard(card);
                }

                break;
            case "HarvestTool":
                if(DroppedSlot != null)
                    board.CropManager.HarvestCrop(DroppedSlot);
                    DroppedSlot = null;
                break;
        }
    }
    private void DropShape(Shape shape, Card card)
    {
        //Get Slots for Shape to plant
        List<BoardSlot> DroppableSlots = GetShapeSlots(shape);

        //If valid plant the new crop
        if (DroppableSlots.Count == shape.NumberOfPlants)
        {
            board.CropManager.PlantCardShape(DroppableSlots, card.Plant);

            Destroy(shape.gameObject);
            Destroy(card.gameObject);
        }
        else
        {
            Destroy(shape.gameObject);
        }

    }

    private List<BoardSlot> GetShapeSlots(Shape shape)
    {
        List<BoardSlot> DroppableSlots = new();
        PointerEventData _pointerData = new PointerEventData(_eventSystem);  

        for (int i = 0; i < shape.transform.childCount; i++)
        {
            Transform squareToCheck = shape.transform.GetChild(i);
            ShapeSquare Square = squareToCheck.gameObject.GetComponent<ShapeSquare>();

            if (Square.Raycast == true)
            {
                List<RaycastResult> raycastResults = new List<RaycastResult>();

                _pointerData.position = Camera.main.WorldToScreenPoint(squareToCheck.position);
                _raycaster.Raycast(_pointerData, raycastResults);

                foreach(var result in raycastResults)
                {
                    if (result.gameObject.tag == "BoardSlot")
                    {
                        if (result.gameObject.transform.childCount == 0)
                        {
                            BoardSlot slot = result.gameObject.GetComponent<BoardSlot>();
                            DroppableSlots.Add(slot);
                        }
                    }
                }
            }
        }

        return DroppableSlots;
    }
}
        // PointerEventData _pointerData = new PointerEventData(_eventSystem);

        // for (int i = 0; i < shape.transform.childCount; i++)
        // {
        //     Transform squareToCheck = shape.transform.GetChild(i);
        //     ShapeSquare Square = squareToCheck.gameObject.GetComponent<ShapeSquare>();

        //     if (Square.Raycast == true)
        //     {
        //         List<RaycastResult> raycastResults = new List<RaycastResult>();

        //         _pointerData.position = Camera.main.WorldToScreenPoint(squareToCheck.position);
        //         _raycaster.Raycast(_pointerData, raycastResults);

        //         foreach(var result in raycastResults)
        //         {
        //             if (result.gameObject.tag == "BoardSlot")
        //             {
        //                 if (result.gameObject.transform.childCount == 0)
        //                 {
        //                     DroppableSlots.Add(result.gameObject);
        //                 }
        //                 else
        //                     isValid = false;
        //             }
        //         }
        //     }
        // }

        // if (isValid)
        // {
        //     foreach (var slot in DroppableSlots)
        //     {
        //         BoardSlot boardSlot = slot.GetComponent<BoardSlot>();
        //         boardSlot.FillSlot(card.Plant, card.cardBase.CardColor);
        //     }
        //     board.GroupManager.ShapeToGroup(DroppableSlots);

        //     Destroy(shape.gameObject);
        //     Destroy(card.gameObject);
        // }
        // else
        // {
        //     Destroy(shape.gameObject);
        // }