using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float damage;
    public float life;

    private float maxlife;
    
    private Animator _anim;
    private Collider _col;
    private NavMeshAgent _agent;

    private Transform _goalTarget;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _col = GetComponent<Collider>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _goalTarget = GameObject.FindWithTag("goal").transform;
        _agent.SetDestination(_goalTarget.position);

        maxlife = life;
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0f)   //el bicho muere
        {
            print(gameObject.name + "muerto.");
            Destroy(gameObject);
        }

        else            //el bicho esta vivo
        {
           // _agent.    
        }
    }

    public void GetHit(float damage)
    {
        life -= damage;
        life = Mathf.Clamp(life, 0, maxlife);
    }

}
