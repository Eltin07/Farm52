using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    public void DiscardCard(Card card)
    {
        Debug.Log("Discarding Card");
        
        GameManager.Instance.ScoreManager.AddDeadPlants();
        Destroy(card.gameObject);
    }
}

