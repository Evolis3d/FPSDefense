using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Projectile : MonoBehaviour
{
    private Collider _col;
    private Animator _anim;
    
    public float speed;
    public float damage;

    private AudioSource _audio;
    
    void Awake()
    {
        _col = GetComponent<Collider>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        _audio.Play();
    }

    void Update()
    {
        transform.Translate(0,0, 1 * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        var layero = other.gameObject.layer;
        if (layero == LayerMask.NameToLayer("Enemies") )
        {
            //var emo = other.GetComponent<Enemy>();
            //emo.GetHit(damage);
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        
    }
    
    
    
}
