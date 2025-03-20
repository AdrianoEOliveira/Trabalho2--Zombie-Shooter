using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject gameController; // Referência ao GameController
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
        if (other.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("Zombie hit!");
            gameController.GetComponent<GameController>().IncreaseEnemiesDefeated();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
