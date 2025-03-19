using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            Destroy(other.gameObject);
        }
    }
}
