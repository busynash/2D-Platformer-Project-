using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    private TextMeshProUGUI coinText;

    public int coinsToGive = 1;

    public AudioClip coinClip;

    private void Start()
    {
        coinText = GameObject.FindWithTag("CoinText").GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Increment the player's coin count
            Player player = collision.gameObject.GetComponent<Player>();

            // PLay coin collect sound
            player.PlaySFX(coinClip, 0.4f);

            // Add 1 to the player's coin count
            player.coins += coinsToGive;

            coinText.text = player.coins.ToString(); //turns integer to string for coinUI
            
            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}
