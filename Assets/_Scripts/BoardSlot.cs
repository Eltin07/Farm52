using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardSlot : MonoBehaviour, IDropHandler
{
    private GameObject SlotFill;
    private Board Board;

    void Start()
    {
        Board = gameObject.GetComponentInParent<Board>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableCard draggableCard = dropped.GetComponent<DraggableCard>();
            Card plantCard = dropped.GetComponent<Card>();

            SlotFill = Instantiate(Board.SlotFillBase, this.transform);
            SlotFill.GetComponent<Image>().color = plantCard.color;
            //Setting new parent destroys gameobject
            draggableCard.parentAfterDrag = transform;
        }
    }
}
