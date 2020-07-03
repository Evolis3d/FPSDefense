using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //lista de oleadas de ese nivel
    public List<SO_Oleada> lista = new List<SO_Oleada>();
    private int _totalWaves;
    private WaveController wc;
    
    
    public float initialDelay;    
    public float timeBetweenWaves;
    
    private float _currentTimer;
    private float _timeInit;
    private bool isRecess = false;
    
    
    //eventos
    public delegate void NotifyWaveStarted(int numWave, int totalWaves);
    public delegate void NotifyWaveEnded(int numWave);
    public delegate void NotifyAllWavesEnded();

    public event NotifyWaveStarted WaveStarted;
    public event NotifyWaveEnded WaveEnded;
    public event NotifyAllWavesEnded AllWavesEnded;

    private void Awake()
    {
        wc = GetComponent<WaveController>();
    }

    private void Start()
    {
        //si hay oleadas en la lista...
        if (lista != null && lista.Count > 0)
        {
            _currentTimer = 0;
            _timeInit = 0;
            
            //saco de dato la cantidad de oleadas que tiene este nivel...
            _totalWaves = lista.Count; 
            
            //limpio elementos vacios de toda la lista de oleadas y preparo sus tamaños...
            TrimNSize();
            
            Invoke("Init", initialDelay);
        }
        else
        {
            throw new NotImplementedException("No hay oleadas asignadas!");
        }
    }

    private void Update()
    {
        // usamos el timer mientras estamos en el descanso entre oleadas
        if (isRecess)
        {
            if (_currentTimer > timeBetweenWaves)
            {
                ResetTimer();
                isRecess = false;
                Init();
            }
            else
            {
                _currentTimer += Time.deltaTime;
            }
        }
    }


    private void Init()
    {
        //siempre trabajamos con el primer elemento de la lista de oleadas, el elemento 0
        wc.Setup(lista[0]);
        wc.InitWave();
        
        WaveStarted?.Invoke(0, lista.Count);
        print("Oleada comenzada!");
        wc.WaveEmpty += () => ClearWaveAt(0);
    }


    private void ClearWaveAt(int indexWave)
    {
        WaveEnded?.Invoke(indexWave);
        print("Oleada " + (indexWave+1) + " de " + _totalWaves + " terminada." );
        wc.WaveEmpty -= () => ClearWaveAt(0);
        lista.RemoveAt(indexWave);
        isRecess = true;
        ResetTimer();
    }

    private void ResetTimer()
    {
        _currentTimer = _timeInit;
    }

    
    //funcion que quita los elementos vacios de cada oleada y genera su cantidad de enemigos según su tipo
    private void TrimNSize()
    {
        foreach (var wave in lista)
        {
            wave.Trim();
            wave.CheckQuantityFromOrder();
        }
    }
    
}
