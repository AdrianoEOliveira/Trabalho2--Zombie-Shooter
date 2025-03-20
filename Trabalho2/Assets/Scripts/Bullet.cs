using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject gameController; // Referência ao GameController
     private bool hasActivated = false;  // Flag para garantir que só execute uma vez
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
        //if (collision.gameObject.tag == "Enemy")
        //{
            //Destroy(collision.gameObject);
        //}
    //}
     private void OnTriggerEnter(Collider other)
    {
        if (hasActivated) return;  // Impede que o código rode mais de uma vez

        if (other.gameObject.CompareTag("Zombie"))
        {
            hasActivated = true;  // Marca como ativado
            Debug.Log("Zombie hit!");
            gameController.GetComponent<GameController>().IncreaseEnemiesDefeated();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            hasActivated = true;  // Marca como ativado também para a parede
            Destroy(gameObject);
        }
    }
}