using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceSystem : MonoBehaviour
{
    public List<Plant_Base> Plants {get; private set; }

    void Awake()
    {
        Plants = Resources.LoadAll<Plant_Base>("Plants").ToList();
    }

    public Plant_Base GetRandomPlant() => Plants[Random.Range(0, Plants.Count)];
}
