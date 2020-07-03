using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum OleadaType
{
    ByQuantity,
    CustomOrder,
}

[CreateAssetMenu(fileName = "Level_xx-Oleada_xx", menuName = "FPSDefense/New Oleada...")]
[System.Serializable]
public class SO_Oleada : ScriptableObject
{
    [Tooltip("Tipo de Oleada a usar: Por cantidad o especificar un orden.")]
    public OleadaType tipo;
    [Tooltip("Pon aquí los prefabs de los enemigos a usar.")]
    public List<GameObject> prefabEnemies = new List<GameObject>();
    [Tooltip("Cantidad de enemigos de esta Oleada.")]
    public int cantidad;
    
    //frecuencia de aparicion
    [Tooltip("Frecuencia de aparición en segs.")]
    public float frecuencia;

    //limpia los elementos vacios de la lista de enemigos, para evitar errores...
    public void Trim()
    {
        prefabEnemies.RemoveAll(x => x == null);
    }

    public void CheckQuantityFromOrder()
    {
        if (tipo == OleadaType.CustomOrder) cantidad = prefabEnemies.Count;
    }
}


