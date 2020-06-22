using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var layero = other.gameObject.layer;
        if (layero == LayerMask.NameToLayer("Enemies") )
        {
            Destroy(gameObject);
            
            
            //GAME OVER, has perdido la partida
        }
    }
}
