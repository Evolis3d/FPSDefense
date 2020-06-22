using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaTorreta : MonoBehaviour
{
    private Animator _anim;
    private Collider _col;
    public float damage;

    private Transform _currentTarget;
    
    //la trampa de Torreta tiene una frecuencia de disparo de x segs
    public float frecuenciaDisparo;
    private float _freqInit = 0;
    private float _currentFreq;
    public GameObject prefabProjectile;
    
    //la trampa de Torreta tiene un tiempo de vida util de X segs.
    public float timeout;
    private float _timeInit = 0;
    private float _currentTime;
    
    
    void Awake()
    {
        _anim = GetComponent<Animator>();
        _col = GetComponent<Collider>();
    }

    void Start()
    {
        _currentTime = _timeInit;
        _currentFreq = _freqInit;
    }

    void Update()
    {
        
        if (_currentTime > timeout)
        {
            Destroy(gameObject); //llamara entonces al OnDestroy...
        }
        else
        {
            _currentTime += Time.deltaTime; 
            
            if (_currentTarget != null)
            {
                ApuntaTorreta(_currentTarget);

                if (_currentFreq > frecuenciaDisparo)
                {
                    _currentFreq = _freqInit;
                    ShootTorreta(_currentTarget); //disparamos un proyectil
                }
                else
                {
                    _currentFreq += Time.deltaTime;
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var layero = other.gameObject.layer;
        if (layero == LayerMask.NameToLayer("Enemies") && _currentTarget == null)
        {
           //establecemos un target para esa torreta y apuntamos
           _currentTarget = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _currentTarget = null;
    }

    private void OnDestroy()
    {
        //destruimos la trampa, anim de desaparecer.
        
        //Avisamos que ya esta libre el slot
    }

    private void ApuntaTorreta(Transform target)
    {
        
    }

    private void ShootTorreta(Transform target)
    {
    }


    private void AbreTrampa()
    {
        _anim.SetTrigger("AbreTrampa");
    }

    private void CierraTrampa()
    {
        _anim.SetTrigger("CierraTrampa"); 
    }

    

}
