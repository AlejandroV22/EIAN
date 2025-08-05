using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPrincipal : MonoBehaviour
{


    public void Empezar()
    {
        SceneManager.LoadScene("VistaPrincipal");
    }

    public void irQuiz()
    {
        SceneManager.LoadScene("MenuQuices");
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aplicación cerrada.");
    }
}
