using UnityEngine;


//script de nexo simple, la partida se pierde al entrar en su trigger
public class Goal : MonoBehaviour
{
    //eventos
    public delegate void NotifyGoalReached(GameObject enemy);
    public event NotifyGoalReached GoalReached;
    
    private void OnTriggerEnter(Collider other)
    {
        var layero = other.gameObject.layer;
        if (layero == LayerMask.NameToLayer("Enemies") )
        {
            Destroy(gameObject);
            
            //GAME OVER, has perdido la partida
            GoalReached?.Invoke(other.gameObject);
        }
    }
}
