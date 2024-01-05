using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    //public Image dragImage;
    [HideInInspector] public Transform parentAfterDrag;
    private Vector3 beforeDragPos;
    private int indexAfterDrag;

    private Vector3 offset = new Vector3(0, 150f, 10.0f);

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        indexAfterDrag = transform.GetSiblingIndex();
        beforeDragPos = transform.position;


        image.raycastTarget = false;
        //dragImage.enabled = true;
        image.enabled = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        var screenPoint = Input.mousePosition;

        screenPoint = screenPoint + new Vector3(0,0,10.0f);// + GetShapeOffset() + offset;
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.SetSiblingIndex(indexAfterDrag);
        transform.position = beforeDragPos;

        image.raycastTarget = true;
        image.enabled = true;
    }

    private Vector3 GetShapeOffset()
    {
        Vector3 shapeOffset = new Vector3(0,0,0);
        RectTransform rt = this.GetComponent<Image>()?.rectTransform;
        if (rt != null)
            shapeOffset = new Vector3(0, rt.rect.height / 2);

        return shapeOffset;
    } 
}
