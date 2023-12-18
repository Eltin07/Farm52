using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Image dragImage;
    [HideInInspector] public Transform parentAfterDrag;
    private Vector3 beforeDragPos;
    private int indexAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        indexAfterDrag = transform.GetSiblingIndex();
        beforeDragPos = transform.position;


        dragImage.raycastTarget = false;
        dragImage.enabled = true;
        image.enabled = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f;
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.SetSiblingIndex(indexAfterDrag);
        transform.position = beforeDragPos;

        dragImage.raycastTarget = true;
        dragImage.enabled = false;
        image.enabled = true;
    }
}
