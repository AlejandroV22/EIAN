using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPrincipal : MonoBehaviour
{
    public void TalloCerebral()
    {
        SceneManager.LoadScene("VistaPrincipal");
    }
    public void AnatCerebral()
    {
        SceneManager.LoadScene("AnatCerebral");
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
     public void Minijuego()
    {
        SceneManager.LoadScene("MenuMinijuegos");
    }
}
