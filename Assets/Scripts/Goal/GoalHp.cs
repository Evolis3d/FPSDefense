using System;
using UnityEngine;


//script de nexo simple, la partida se pierde al quitarle toda su vida..
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
            
            //resto vida al Goal y mato al bicho, le daño con su total de vida
            Enemy es = other.transform.GetComponent<Enemy>();
            
            GetHit(es.damage);
            es.GetHit(es.life);
            
            
        }
    }

    public void GetHit(float damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, _maxHp);
        GoalHit?.Invoke(damage);
        
        //si no le queda vida, la destruyo y notifico
        if (hp == 0)
        {
            GoalDestroyed?.Invoke();
            print("Torre Destruida. GAME OVER");
        }
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
        return (hp == 0);
    }

}
