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
    public Plant_Base plant;
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
        plant = Systems.Instance.ResourceSystem.GetRandomPlant();
        color = plant.color;
        image.color = color;
    }
}
