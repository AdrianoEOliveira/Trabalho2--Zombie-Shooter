using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Button again;
    //[SerializeField] private Button botao2Players;
    //[SerializeField] private Button botao3Players;

    private void Start()
    {
        //time = GameObject.FindWithTag("Time").GetComponent<TextMeshProUGUI>();
        //scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
        
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;  

        again.onClick.AddListener(() => jogarDeNovo());
        float survivalTime = PlayerPrefs.GetFloat("SurvivalTime", 0f);  // Recupera o tempo de sobrevivência
        int enemiesDefeated = PlayerPrefs.GetInt("EnemiesDefeated", 0);  // Recupera o número de inimigos derrotados
        time.text = "Tempo de Sobrevivência: " + survivalTime.ToString("F2") + "s";  // Exibe o tempo de sobrevivência
        scoreText.text = "Inimigos Derrotados: " + enemiesDefeated;  // Exibe o número de inimigos derrotados
        //botao2Players.onClick.AddListener(() => IniciarJogo2());
        //botao3Players.onClick.AddListener(() => IniciarJogo3());
    }

    public void jogarDeNovo()
    {
        SceneManager.LoadScene("Stage"); 
    }

}
