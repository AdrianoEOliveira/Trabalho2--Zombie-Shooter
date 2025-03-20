using UnityEngine;
using TMPro;

public class playerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isMoving;

    private Vector3 lastPosition = Vector3.zero;

    private int vidas = 3;

    public bool isDeath = false;

    [SerializeField] private TextMeshPro vidasText; // Referência ao TextMeshPro de balas

    public GameObject gameController; // Referência ao GameController



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        vidasText = GameObject.FindWithTag("Vidas").GetComponent<TextMeshPro>();
        atualziarVidas();
        gameController = GameObject.FindWithTag("GameController");
        
    }

    // Update is called once per frame
    void Update()
    {
        LogicaMovimento();
        Death();  
    }
    
    public void aplicarDano()
    {
        if(vidas>0)
        {
            vidas--;
            atualziarVidas();
            gameController.GetComponent<GameController>().DecreasePlayerLife();
            Debug.Log("Vidas: " + vidas);
            if(vidas == 0)
            {
                isDeath = true;
            }
        }
    }

    void Death()
    {
        if(isDeath)
        {
            Debug.Log("Game Over");
            gameController.GetComponent<GameController>().GameOver();
        }
    }


    void LogicaMovimento()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (lastPosition != transform.position && isGrounded == true)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = transform.position;
    }

    void atualziarVidas()
    {
        vidasText.text = "Vidas: " + vidas + "/3";
        
    }
}
