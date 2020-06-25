using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaSlots : MonoBehaviour
{
    
    public List<Transform> Slots = new List<Transform>();

    void Start()
    {
        if (Slots != null && Slots.Count >= 1) return;
        var tempo = GameObject.FindGameObjectsWithTag("slot");
        foreach (GameObject go in tempo)
        {
            Slots.Add(go.transform);
        }
    }
}
