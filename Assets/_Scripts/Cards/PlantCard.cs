using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlantCard : MonoBehaviour
{
    public Plant plantBase;
    public Image cardImage;

    void Start()
    {
        plantBase = Systems.Instance.ResourceSystem.GetRandomPlant();
    }

    private void Setup()
    {
        
    }

}
