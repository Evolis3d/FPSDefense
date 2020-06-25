using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//ejemplo de oleada con un solo tipo de enemigo
public class OleadaSingle : MonoBehaviour
{

    //puntos de salida de los enemigos para esta oleada
    public List<Transform> spawnPoints = new List<Transform>();
    
    //Tipo de enemigo de la oleada...
    public GameObject enemyPrefab;
    public int cantidad;
    private int _restantes;
    private Transform _rootEnemies;
    
    
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
        if ((spawnPoints.Count + cantidad > 0) && (enemyPrefab != null))
        {
            _freqcurrentTimer = 0;
            _freqInitTimer = 0;
            _restantes = cantidad;
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
        var r = Random.Range(0, spawnPoints.Count - 1);
        var clon = Instantiate(enemyPrefab, spawnPoints[r]);
        clon.transform.SetParent(_rootEnemies);
        //reduzco la cantidad de los que quedan por salir
        _restantes--;
        //notifico evento de enemigo lanzado y desde donde..
        EnemySpawned?.Invoke(clon, spawnPoints[r]); 
    }


}
