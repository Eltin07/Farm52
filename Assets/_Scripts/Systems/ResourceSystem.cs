using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceSystem : MonoBehaviour
{
    public List<Card_Base> Cards {get; private set; }
    public List<Shape_Base> Shapes { get; private set; }

    void Awake()
    {
        Cards = Resources.LoadAll<Card_Base>("Cards").ToList();
        Shapes = Resources.LoadAll<Shape_Base>("Shapes").ToList();
    }

    public Card_Base GetRandomCard() => Cards[Random.Range(0, Cards.Count)];
    public Shape_Base GetRandomShape() => Shapes[Random.Range(0, Shapes.Count)];
}
