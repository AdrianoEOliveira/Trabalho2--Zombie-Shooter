using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private Button Inicio;
    //[SerializeField] private Button botao2Players;
    //[SerializeField] private Button botao3Players;

    private void Start()
    {
        Inicio.onClick.AddListener(() => Iniciar());
        //botao2Players.onClick.AddListener(() => IniciarJogo2());
        //botao3Players.onClick.AddListener(() => IniciarJogo3());
    }

    public void Iniciar()
    {
        SceneManager.LoadScene("Stage"); 
    }

}
