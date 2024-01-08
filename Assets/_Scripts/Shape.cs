using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public Sprite DisplayImage;
    public int NumberOfPlants;

    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool isPlant = transform.GetChild(i).GetComponent<ShapeSquare>().Raycast;
            if(isPlant)
                NumberOfPlants++;
        }
    }
}
