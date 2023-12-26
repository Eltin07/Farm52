using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Image image;
    public delegate void CardPlayed();
    public static event CardPlayed OnCardPlayed;
    public Color32 color;
    public Card_Base card;
    //Card is desrtroyed when played, send out event saying card was played
    void OnDestroy() 
    {
        OnCardPlayed?.Invoke();
    }

    void OnTransformParentChanged()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        card = Systems.Instance.ResourceSystem.GetRandomCard();
        color = card.HexColor;
        image.color = color;
    }
}
