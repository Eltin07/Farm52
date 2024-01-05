using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceSystem : MonoBehaviour
{
    public List<Card_Base> Cards {get; private set; }
    public List<GameObject> Shapes { get; private set; }
    public List<Plant> Plants { get; private set; }

    void Awake()
    {
        Cards = Resources.LoadAll<Card_Base>("Cards").ToList();
        Shapes = Resources.LoadAll<GameObject>("Shapes").ToList();
        Plants = Resources.LoadAll<Plant>("Plants").ToList();
    }

    public Card_Base GetRandomCard() => Cards[Random.Range(0, Cards.Count)];
    public Plant GetRandomPlant() => Plants[Random.Range(0, Plants.Count)];
    public GameObject GetRandomShape() => Shapes[Random.Range(0, Shapes.Count)];
}
