using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;  // Prefab do inimigo
    public Transform enemySpawnPoint;  // Ponto de spawn do inimigo
    public GameObject player;  // Referência ao jogador

    private float spawnInterval = 5f;  // Intervalo inicial de spawn (5 segundos)
    private float spawnTimer = 0f;  // Timer de contagem para o próximo spawn
    private float spawnSpeedMultiplier = 1.1f;  // Multiplicador para aumentar a velocidade

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // Encontra o jogador na cena
        if (player == null)
        {
            Debug.LogError("Player não atribuído!");
        }
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        

        // Verifica se o tempo de spawn foi atingido
        if (spawnTimer >= spawnInterval && !IsPlayerNearby())
        {
            SpawnEnemy();
            spawnTimer = 0f;  // Reseta o timer

            // Aumenta a velocidade de spawn a cada intervalo de tempo
            IncreaseSpawnSpeed();
        }
    }

    void SpawnEnemy()
    {
        // Instancia o inimigo no ponto de spawn
        Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
    }

    void IncreaseSpawnSpeed()
    {
        // Aumenta a velocidade de spawn com o tempo
        spawnInterval /= spawnSpeedMultiplier;
        spawnInterval = Mathf.Max(spawnInterval, 1f);  // Garante que o intervalo de spawn não fique menor que 1 segundo
    }

    bool IsPlayerNearby()
    {
        // Verifica se o jogador está perto do ponto de spawn
        float distanceToPlayer = Vector3.Distance(player.transform.position, enemySpawnPoint.position);
        return distanceToPlayer < 10f;  // Considera que o jogador está perto se estiver a menos de 10 unidades de distância
    }
}
