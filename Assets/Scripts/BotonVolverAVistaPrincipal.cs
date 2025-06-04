using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonVolverAVistaPrincipal : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string nombreEscena;

    public void Volver()
    {
        if (!string.IsNullOrEmpty(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el nombre de la escena en el Inspector.");
        }
    }
}
