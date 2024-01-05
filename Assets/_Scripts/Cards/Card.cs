using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour//, IPointerClickHandler
{

    public Image Image;
    public Image ShapeImage;
    public Color32 color;
    public Card_Base cardBase;
    public GameObject Shape;
    public Plant Plant;


    #region Events
    public delegate void CardPlayed();
    public static event CardPlayed OnCardPlayed;
    #endregion

    // public void OnPointerClick(PointerEventData eventData)
    // {
    //     int clickCount = eventData.clickCount;

    //     ShapeImage.rectTransform.Rotate(new Vector3(0,0, 90 * clickCount));
    // }
    void OnDestroy() 
    {
        OnCardPlayed?.Invoke();
    }

    void Start()
    {
        cardBase = Systems.Instance.ResourceSystem.GetRandomCard();
        Shape = Systems.Instance.ResourceSystem.GetRandomShape();
        Plant = cardBase.Plant;
        Setup();
    }


    private void Setup()
    {
        Image.color = cardBase.HexColor;
    }

    public void RotateShape()
    {
        ShapeImage.rectTransform.Rotate(new Vector3(0,0, 90));
    }
}
