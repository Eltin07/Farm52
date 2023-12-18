using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systems : MonoBehaviour
{
    public static Systems Instance { get; private set; }

    public ResourceSystem ResourceSystem { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        ResourceSystem = GetComponentInChildren<ResourceSystem>();
    }
}
