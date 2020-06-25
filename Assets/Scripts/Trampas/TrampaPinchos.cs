using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaPinchos : MonoBehaviour
{
    private Animator _anim;
    private Collider _col;
    public float damage;

    private bool _occupied = false;
    
    //la trampa de pinchos tiene un tiempo de vida util de X segs.
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
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var layero = other.gameObject.layer;
        if (layero != LayerMask.NameToLayer("Enemies") || _occupied != false) return;
        AbreTrampa();
        //var emo = other.GetComponent<Enemy>();
        //emo.GetHit(damage);
        _occupied = true;
    }

    private void OnDestroy()
    {
        //destruimos la trampa, anim de desaparecer.
        
        //Avisamos que ya esta libre el slot
    }


    private void AbreTrampa()
    {
        _anim.SetTrigger("AbreTrampa");
    }

    private void CierraTrampa()
    {
       _anim.SetTrigger("CierraTrampa"); 
    }

    private void SetOccupied(bool estado)
    {
        _occupied = estado;
    }

}
