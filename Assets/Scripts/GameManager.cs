using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public GameObject gameOverPanel;
    
    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Remettre le temps à la normale au démarrage
        Time.timeScale = 1f;
    }
    
    void Start()
    {
        // Cacher le panel game over au début
        gameOverPanel.SetActive(false);
        UpdateScoreText();
    }
    
    public void AddPoint()
    {
        score++;
        Debug.Log("Point ajouté! Score actuel: " + score);
        UpdateScoreText();
    }
    
    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
    
    public void GameOver()
    {
        // Arrêter le temps
        Time.timeScale = 0f;
        Debug.Log("Game Over appelé!");
        
        // Afficher le panel game over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            
            // Mettre à jour le score final
            if (finalScoreText != null)
                finalScoreText.text = "Score Final: " + score;
        }
    }
    
    public void RestartGame()
    {
        // Recharger la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitGame()
    {
        // Quitter le jeu
        Application.Quit();
        
        // Message pour l'éditeur Unity
        Debug.Log("Quitter le jeu");
    }
}