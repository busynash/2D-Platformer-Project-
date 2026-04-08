using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Increment the player's coin count
            Player player = collision.gameObject.GetComponent<Player>();

            // Add 1 to the player's coin count
            player.coins += 1;
            
            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}
