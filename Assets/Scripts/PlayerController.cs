using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 1.5f;
    
    private Rigidbody2D rb;
    private bool isGrounded = false;
    
    public AudioClip coinSound;
    private AudioSource audioSource;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Pour la détection du sol
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
        
        // Pour la détection des obstacles SOLIDES (non triggers)
        if (collision.collider.CompareTag("Obstacle"))
        {
            GameManager.instance.GameOver();
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // SUPPRIMÉ la logique de collecte des pièces d'ici
        // Elle est maintenant gérée par le script CoinCollector sur chaque pièce
        
        // Pour les obstacles en trigger
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Collision avec obstacle détectée: " + other.gameObject.name);
            GameManager.instance.GameOver();
        }
    }
}