using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaMina : MonoBehaviour
{
    private bool _occupied;
    private Animator _anim;

    public float damage;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _occupied = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var layero = other.gameObject.layer;
        if (layero == LayerMask.NameToLayer("Enemies") && _occupied == false)
        {
            AbreTrampa();
            //var emo = other.GetComponent<Enemy>();
            //emo.GetHit(damage);
            _occupied = true;
        }
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
    
    
    private void SetOccupied(bool estado)
    {
        _occupied = estado;
    }
}
