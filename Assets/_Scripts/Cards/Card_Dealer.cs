using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Dealer : MonoBehaviour
{
    public GameObject CardBase;
    private List<GameObject> dealtCards;

    void Awake() => Card.OnCardPlayed += DealNewCard;
    void OnDestroy() => Card.OnCardPlayed -= DealNewCard;

    void Start()
    {
        DealStarterCards();
    }

    private void DealStarterCards()
    {
        for(int i=0; i < 3; i++)
        {
            Instantiate(CardBase, this.transform);
        }
    }
    private void DealNewCard()
    {
        Instantiate(CardBase, this.transform);
    }
}
