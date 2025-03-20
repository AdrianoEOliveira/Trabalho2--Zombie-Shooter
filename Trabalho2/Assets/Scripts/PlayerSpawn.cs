using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;  // Prefab do jogador
    public Transform playerSpawnPoint;  // Transform do ponto de spawn

    void Start()
    {
        if (playerPrefab != null && playerSpawnPoint != null)
        {
            // Instancia o jogador na posição do ponto de spawn
            Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        }
        else
        {
            Debug.LogError("PlayerPrefab ou PlayerSpawnPoint não atribuídos!");
        }
    }
}
