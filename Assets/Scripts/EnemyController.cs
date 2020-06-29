using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private OleadaGenerica _oleada;

    [SerializeField] private List<Transform> enemies = new List<Transform>();
    
    void Awake()
    {
        _oleada = GetComponent<OleadaGenerica>();
    }

    void Start()
    {
        _oleada.EnemySpawned += EnemigoLanzado;
        _oleada.WaveEmpty += OleadaVacia;
    }

    void OnDestroy()
    {
        _oleada.EnemySpawned -= EnemigoLanzado;
        _oleada.WaveEmpty -= OleadaVacia;
    }

    private void EnemigoLanzado(GameObject ene, Transform spawn)
    {
        print("Ha salido un " + ene.transform.name + " del punto " +  spawn.name );
        enemies.Add(ene.transform);
    }

    private void OleadaVacia()
    {
        print("Ya no quedan más enemigos por lanzar");
    }
    
}
