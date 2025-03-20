using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int playerLives = 3;  // Número de vidas do jogador
    public float survivalTime = 0f;  // Tempo de sobrevivência
    public int enemiesDefeated = 0;  // Contador de inimigos derrotados

    private bool isGameOver = false;  // Flag para verificar se o jogo acabou

    void Update()
    {
        // Verificar se o jogo terminou (vidas acabaram)
        if (playerLives <= 0 && !isGameOver)
        {
            GameOver();
        }
        else if (!isGameOver)
        {
            // Aumentar o tempo de sobrevivência enquanto o jogo está rodando
            survivalTime += Time.deltaTime;
        }
    }

    // Método para diminuir as vidas do jogador quando ele perde uma vida
    public void DecreasePlayerLife()
    {
        if (playerLives > 0)
        {
            playerLives--;
        }
    }

    // Método para incrementar o contador de inimigos derrotados
    public void IncreaseEnemiesDefeated()
    {
        enemiesDefeated++;
    }

    // Método para lidar com o fim do jogo
    private void GameOver()
    {
        isGameOver = true;
        // Carregar a cena de score e passar os dados para ela
        PlayerPrefs.SetInt("EnemiesDefeated", enemiesDefeated);  // Armazena o número de inimigos derrotados
        PlayerPrefs.SetFloat("SurvivalTime", survivalTime);  // Armazena o tempo de sobrevivência
        SceneManager.LoadScene("ScoreScene", LoadSceneMode.Single);
    }
}
