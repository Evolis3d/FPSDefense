using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using Random = UnityEngine.Random;

//ejemplo de oleada de random de enemigos
public class OleadaRandom : MonoBehaviour
{

    //puntos de salida de los enemigos para esta oleada
    public List<Transform> spawnPoints = new List<Transform>();
    
    //lista de enemigos de la oleada...
    public List<GameObject> PrefabEnemies = new List<GameObject>();
    private Transform _rootEnemies;
    public int Cantidad;
    private int _restantes;
    
    
    //eventos
    public delegate void NotifyEnemySpawned(GameObject enemy, Transform spawn);
    public event NotifyEnemySpawned EnemySpawned;

    public delegate void NotifyWaveEmpty();
    public event NotifyWaveEmpty WaveEmpty;
    private bool _waveIsEmpty = false;
    
    
    //frecuencia de aparicion entre enemigos
    public float freqEnter;
    private float _freqcurrentTimer;
    private float _freqInitTimer;


    
    //nos avisa cuando ya no quedan enemigos en la lista
    private bool AllEnemiesOut()
    {
        return (_restantes == 0);
    }

    void Awake()
    {
        SacaSpawns();
        _rootEnemies = GameObject.Find("Enemies").transform;
    }


    void Start()
    {
        if ((spawnPoints.Count + PrefabEnemies.Count + Cantidad > 0) && (PrefabEnemies!=null))
        {
            _freqcurrentTimer = 0;
            _freqInitTimer = 0;
            _restantes = Cantidad;
        }
        else
        {
            throw  new NotImplementedException("No hay ni enemigos ni spawnpoints.");
        }
    }

    void Update()
    {
        //cuando llega la alarma, lanzamos un enemigo de la lista
        if (_freqcurrentTimer > freqEnter)
        {
            _freqcurrentTimer = _freqInitTimer;
            
            //mientras haya enemigos en la ola, instanciamos, o reciclamos de la pool
            if (!AllEnemiesOut())
            {
              LanzaEnemigo();  
            }
            else  //Ya no quedan más enemigos, notificamos...
            {
                if (_waveIsEmpty) return;
                WaveEmpty?.Invoke();
                _waveIsEmpty = true;
            }
        }
        else
        {
            _freqcurrentTimer += Time.deltaTime;
        }
    }

    private bool NoSpawnPointsInlevel()
    {
        //busca como minimo 1 spawnpoint en la escena
        var sp = GameObject.FindGameObjectWithTag("spawnpoint");
        return (sp == null);
    }

    private void SacaSpawns()
    {
        if (!NoSpawnPointsInlevel())  //si hay spawns en el level...
        {
            //si no hay en la lista dada, la relleno, sino la aprovecho...
            if (spawnPoints.Count >= 1) return;
            //buscamos objetos con el Tag "spawnpoint"...
            var sp = GameObject.FindGameObjectsWithTag("spawnpoint");
            foreach (GameObject point in sp)
            {
                spawnPoints.Add(point.transform);
            }
        } 
        else
        {
            throw new NotImplementedException("Se necesita como minimo un spawnPoint de Enemigos.");
        }
    }

    private void LanzaEnemigo()
    {
        var n = Random.Range(0, PrefabEnemies.Count - 1);
        var r = Random.Range(0, spawnPoints.Count - 1);
        var clon = Instantiate(PrefabEnemies[n], spawnPoints[r]);
        clon.transform.SetParent(_rootEnemies);
        //limpio ese elemento de la lista
        _restantes--;
        //notifico evento de enemigo lanzado y desde donde..
        EnemySpawned?.Invoke(clon, spawnPoints[r]); 
    }


}
