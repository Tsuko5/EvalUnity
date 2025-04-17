using UnityEngine;

// Créez un nouveau script à attacher aux pièces
public class CoinCollector : MonoBehaviour
{
    private bool isCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected) return;
        
        if (other.CompareTag("Player"))
        {
            isCollected = true;
            AudioSource playerAudio = other.GetComponent<AudioSource>();
            PlayerController player = other.GetComponent<PlayerController>();
            
            if (player != null && playerAudio != null && player.coinSound != null)
                playerAudio.PlayOneShot(player.coinSound);
            
            // Ajouter le point
            GameManager.instance.AddPoint();
            
            // Désactiver visuellement la pièce
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            
            // Détruire l'objet après un court délai
            Destroy(gameObject, 0.1f);
        }
    }
}