using UnityEngine;
using UnityEngine.AI; // Necessário para o NavMesh

public class AIController : MonoBehaviour
{
    [Header("AI Settings")]
    public Transform target; // Alvo (Jogador)

    public GameObject player;
    public float chaseRange = 10f; // Distância para começar a perseguição
    public float damage = 10f; // Dano causado ao jogador
    public float damageCooldown = 1f; // Tempo entre cada aplicação de dano

    private NavMeshAgent agent; // Referência ao NavMeshAgent
    private float nextDamageTime; // Controle do tempo entre danos

    // Start é chamado antes da primeira atualização do frame
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Procura automaticamente o jogador pela tag "Player"
        if (target == null && GameObject.FindWithTag("Player") != null)
        {
            target = GameObject.FindWithTag("Player").transform;
            player = GameObject.FindWithTag("Player");
        }
    }

    // Update é chamado a cada frame
    void Update()
    {
        if (target != null)
        {
            // Sempre persegue o jogador
            agent.SetDestination(target.position);
        }
    }

    // Detecta contato físico com o jogador
     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Corpo"))
        {
            if (Time.time >= nextDamageTime)
            {
                nextDamageTime = Time.time + damageCooldown; // Controla a frequência do dano
                player.GetComponent<playerMovement>().aplicarDano();
                Debug.Log("Player hit!");  
            }
        }
    }

}
