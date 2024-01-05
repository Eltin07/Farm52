using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject Shape;
    public Card Card;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Card = gameObject.GetComponent<Card>(); 
        Quaternion ShapeRotation = Card.ShapeImage.rectTransform.rotation;
        Shape = Instantiate(Card.Shape, Input.mousePosition, ShapeRotation, this.transform);

        Card.ShapeImage.enabled = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        var screenPoint = Input.mousePosition;

        screenPoint = screenPoint + new Vector3(0,0,10.0f);
        Shape.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Card.ShapeImage.enabled = true;
        Destroy(Shape);
    }
}
