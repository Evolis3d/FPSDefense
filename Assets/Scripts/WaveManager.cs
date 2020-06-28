using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //lista de oleadas de ese nivel
    public List<Oleada> lista = new List<Oleada>();
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

    void Start()
    {
        //si hay oleadas en la lista...
        if (lista != null && lista.Count > 0)
        {
            _currentTimer = 0;
            _timeInit = 0;
            Invoke("Init", initialDelay);
        }
        else
        {
            throw new NotImplementedException("No hay oleadas asignadas!");
        }
    }

    void Update()
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


    void Init()
    {
        //lista[0].init();
        WaveStarted?.Invoke(0, lista.Count);
        lista[0].WaveEmpty += () => ClearWaveAt(0);
    }

    
    void ClearWaveAt(int indexWave)
    {
        WaveEnded?.Invoke(indexWave);
        print("Oleada " + indexWave + "terminada." );
        lista[indexWave].WaveEmpty -= () => ClearWaveAt(0);
        lista.RemoveAt(indexWave);
        isRecess = true;
        ResetTimer();
    }

    void ResetTimer()
    {
        _currentTimer = _timeInit;
    }
    
}
