using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;


public class MapGenerator : MonoBehaviour
{
    [Header("Configurações")]
    public int maxIteracoes = 200;
    public int stepDistance = 10;
    public GameObject roomPrefab;
    public GameObject roomNorthWallPrefab;
    public GameObject roomSouthWallPrefab;
    public GameObject roomWestWallPrefab;
    public GameObject roomEastWallPrefab;

    public GameObject playerSpawnPrefab;

    public GameObject enemySpawnPrefab;

    public float alturaSalas = 0f;

    [Header("Debug")]
    [SerializeField] private Vector3Int _currentPosition;
    private HashSet<Vector3Int> _floorPositions = new HashSet<Vector3Int>();

    [Header("NavMesh")]
    public NavMeshSurface surface;

    private Vector3Int[] directions = {
        Vector3Int.right,
        Vector3Int.left,
        Vector3Int.forward,
        Vector3Int.back
    };

    void Awake()
    {
    playerSpawnPrefab = Resources.Load<GameObject>("Prefabs/PlayerSpawn");
    enemySpawnPrefab = Resources.Load<GameObject>("Prefabs/EnemySpawn");
    GenerateMap();
    }

    //void Start()
    //{
        //GenerateMap();
    //}

    public void GenerateMap()
    {
        _currentPosition = Vector3Int.zero;
        _floorPositions.Clear();
        _floorPositions.Add(_currentPosition);

        for (int n = 0; n < maxIteracoes; n++)
        {
            Vector3Int randomDirection = directions[Random.Range(0, directions.Length)];
            _currentPosition += randomDirection * stepDistance;
            _floorPositions.Add(_currentPosition);
        }

        GenerateRooms();

        if (surface != null)
        {
            surface.BuildNavMesh();
        }
    }

private void GenerateRooms()
{
    List<Vector3Int> availablePositions = new List<Vector3Int>(); // Lista para armazenar as posições das salas
    List<Vector3Int> enemySpawnPositions = new List<Vector3Int>(); // Lista para armazenar as posições de spawn dos inimigos

    foreach (Vector3Int position in _floorPositions)
    {
        availablePositions.Add(position);
    }
    Debug.Log("Available positions: " + availablePositions.Count);

    // Escolher uma sala aleatória para o spawn do jogador
    Vector3Int playerSpawnPosition = availablePositions[Random.Range(0, availablePositions.Count)];
    availablePositions.Remove(playerSpawnPosition); // Remover a posição do jogador da lista de posições disponíveis

    // Instanciar a sala do jogador
    Vector3 playerSpawnWorldPosition = new Vector3(playerSpawnPosition.x, alturaSalas+2, playerSpawnPosition.z);
    Instantiate(playerSpawnPrefab, playerSpawnWorldPosition, Quaternion.identity, transform); // Posição do jogador
    //player.name = "Player";

    // Escolher salas para inimigos, garantindo que fiquem longe do jogador
    while (enemySpawnPositions.Count < 5) // Número de inimigos a serem gerados
    {
        Vector3Int enemySpawnPosition = availablePositions[Random.Range(0, availablePositions.Count)];
        float distanceToPlayer = Vector3Int.Distance(playerSpawnPosition, enemySpawnPosition);

        if (distanceToPlayer > 10) // Garantir que os inimigos fiquem suficientemente distantes do jogador
        {
            enemySpawnPositions.Add(enemySpawnPosition);
            availablePositions.Remove(enemySpawnPosition); // Remover a posição de spawn do inimigo da lista de disponíveis

            // Instanciar inimigo na posição escolhida
            Vector3 enemySpawnWorldPosition = new Vector3(enemySpawnPosition.x, alturaSalas + 0.18f, enemySpawnPosition.z);
            Instantiate(enemySpawnPrefab, enemySpawnWorldPosition, Quaternion.identity, transform); // Posição do inimigo
            //enemy.name = "Enemy";

            // Adicione o código para configurar o inimigo aqui
        }
    }

    // Agora instanciar as salas e as paredes conforme o código original
    foreach (Vector3Int position in _floorPositions)
    {
        Vector3 spawnPosition = new Vector3(position.x, alturaSalas, position.z);

        // Verificar vizinhos para definir qual prefab usar
        bool hasNorthNeighbor = _floorPositions.Contains(position + Vector3Int.forward * stepDistance);
        bool hasSouthNeighbor = _floorPositions.Contains(position + Vector3Int.back * stepDistance);
        bool hasEastNeighbor = _floorPositions.Contains(position + Vector3Int.right * stepDistance);
        bool hasWestNeighbor = _floorPositions.Contains(position + Vector3Int.left * stepDistance);

        // Ajuste de posição para as paredes com base no prefab da parede
        float wallOffset = 5f; // Ajuste de meio passo (metade da distância do passo)

        // Posicionar as paredes corretamente, mas sem sobrepor a sala
        if (!hasNorthNeighbor)
        {
            Vector3 northWallPosition = spawnPosition + Vector3.forward * wallOffset;
            Instantiate(roomNorthWallPrefab, northWallPosition, Quaternion.Euler(0, 0, 0), transform);
        }
        if (!hasSouthNeighbor)
        {
            Vector3 southWallPosition = spawnPosition + Vector3.back * wallOffset;
            Instantiate(roomSouthWallPrefab, southWallPosition, Quaternion.Euler(0, 180, 0), transform);
        }
        if (!hasEastNeighbor)
        {
            Vector3 eastWallPosition = spawnPosition + Vector3.right * wallOffset;
            Instantiate(roomEastWallPrefab, eastWallPosition, Quaternion.Euler(0, 90, 0), transform);
        }
        if (!hasWestNeighbor)
        {
            Vector3 westWallPosition = spawnPosition + Vector3.left * wallOffset;
            Instantiate(roomWestWallPrefab, westWallPosition, Quaternion.Euler(0, -90, 0), transform);
        }

        // Instancia a sala no centro
        GameObject selectedPrefab = roomPrefab;
        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity, transform);
    }
}


        //[ContextMenu("Gerar Novo Mapa XZ")]
        //private void GenerateNewMap()
        //{
            //GenerateMap();
        //}
    }
