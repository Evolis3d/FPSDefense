using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


//WaveController controla la currentWave que tiene ahora mismo la WaveManager...
[RequireComponent(typeof(WaveManager))]
public class WaveController : MonoBehaviour
{
    private SO_Oleada _current;
    
    private List<Transform> spawnPoints = new List<Transform>();
    private Transform _rootEnemies;
    private int _restantes;
    private bool _waveIsEmpty = false;
    private float _freqCurrentTimer;
    private float _freqInitTimer;
    
    //eventos
    public delegate void NotifyEnemySpawned(GameObject enemy, Transform spawn);
    public delegate void NotifyWaveEmpty();
    public event NotifyEnemySpawned EnemySpawned;
    public event NotifyWaveEmpty WaveEmpty;

    
    //funcion publica que sustitye al Awake() para asignar y preparar al principio la Oleada seleccionada del WaveManager
    public void Setup(SO_Oleada wave)
    {
        _current = wave;
        
        //Regenero la lista de spawnpoints, porque se pueden haber añadido más según el nivel...
        
        SacaSpawns();
        
        //preparo el parent donde se almacenaran  los enemigos en la escena
        _rootEnemies = GameObject.Find("Enemies").transform;
    }

    private void Start() { }

    private void Init()
    {
        if ((spawnPoints.Count + _current.prefabEnemies.Count + _current.cantidad > 0) && (_current != null))
        {
            _freqInitTimer = 0;
            _freqCurrentTimer = 0;
            _restantes = _current.cantidad;
        }
        else
        {
            throw new NotImplementedException("No hay ni enemigos ni spawnpoints");
        }
    }

    //Funcion publica para ser llamada desde el WaveManager...
    public void InitWave()
    {
        Init();
    }

    private void Update()
    {
        if (!_current) return;
        if (_freqCurrentTimer > _current.frecuencia)
        {
            _freqCurrentTimer = _freqInitTimer;

            //mientras haya enemigos en la ola, instanciamos, o reciclamos de la pool
            if (!AllEnemiesOut())
            {
                LanzaEnemigo();
            }
            else //Ya no quedan más enemigos, notificamos...
            {
                if (_waveIsEmpty) return;
                WaveEmpty?.Invoke();
                _waveIsEmpty = true;
            }
        }
        else
        {
            _freqCurrentTimer += Time.deltaTime;
        }
    }

    //ya no quedan enemigos en la lista...
    private bool AllEnemiesOut()
    {
        /*
        var result = false;
        if (_current != null && _current.prefabEnemies != null)
        {
            result = (_current.prefabEnemies.Count == 0 || _restantes == 0);
        }
        return result;
        */
        return (_restantes == 0);
    }

    private bool NoSpawnPointsInLevel()
    {
        //busca como minimo 1 spawnpoint en la escena
        var sp = GameObject.FindGameObjectWithTag("spawnpoint");
        return (sp == null);
    }

    private void SacaSpawns()
    {
        //primero limpio los elementos vacios de la lista, si los hay, evito errores...
        spawnPoints.RemoveAll(x => x == null);
        
        if (!NoSpawnPointsInLevel())
        {
            //si no hay en la lista dada, la relleno, sino la aprovecho...
            if (spawnPoints.Count >= 1) return;

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
        var n = Random.Range(0, _current.prefabEnemies.Count - 1);
        var r = Random.Range(0, spawnPoints.Count - 1);
        
        //logica segun el tipo de oleada
        if (_current.tipo == OleadaType.CustomOrder) n = 0;

        var clon = Instantiate(_current.prefabEnemies[n], spawnPoints[r]);
        clon.transform.SetParent(_rootEnemies);
        
        //limpio ese elemento de la lista, nos aseguramos en el InGame...
        if (_current.tipo == OleadaType.CustomOrder) _current.prefabEnemies.RemoveAt(0);
        _restantes--;
        
        //notifico evento de enemigo lanzado y desde donde...
        EnemySpawned?.Invoke(clon, spawnPoints[r]);
    }

}
