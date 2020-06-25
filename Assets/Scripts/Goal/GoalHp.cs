﻿using System;
using UnityEngine;


//script de nexo simple, la partida se pierde al entrar en su trigger
public class GoalHp : MonoBehaviour
{
    public float hp;
    private float _maxHp;
    
    //eventos
    public delegate void NotifyGoalReached(GameObject enemy);
    public event NotifyGoalReached GoalReached;

    public delegate void NotifyGoalDestroyed();
    public event NotifyGoalDestroyed GoalDestroyed;

    public delegate void NotifyGoalRegenerated();
    public event NotifyGoalRegenerated GoalRegenerated;

    public delegate void NotifyGoalHit(float damage);
    public event NotifyGoalHit GoalHit;
    

    void Start()
    {
        _maxHp = hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        var layero = other.gameObject.layer;
        if (layero == LayerMask.NameToLayer("Enemies") )
        {
            //un bicho ha llegado hasta el goal
            GoalReached?.Invoke(other.gameObject);
        }
    }

    public void GetHit(float damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, _maxHp);
        GoalHit?.Invoke(damage);
    }

    public void SetHp(float amount)
    {
        hp = Mathf.Clamp(amount, 0, _maxHp);
    }

    public void Regenerate()
    {
        hp = Mathf.Clamp(_maxHp, 0, _maxHp);
        GoalRegenerated?.Invoke();
    }

    public bool isFull()
    {
        return (hp == _maxHp);
    }

    public bool isDead()
    {
        GoalDestroyed?.Invoke();
        return (hp == 0);
    }

}
