using UnityEngine;

public class Collectable : MonoBehaviour
{

    [SerializeField] private int points = 10;

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){

            Player player = other.GetComponentInParent<Player>();
            player.AddPoints(points);
            Destroy(gameObject);
        }
    }

}