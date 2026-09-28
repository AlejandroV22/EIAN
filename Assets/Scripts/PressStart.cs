using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class PressStart : MonoBehaviour
{
    [Header("Configuración de Escena")]
    [Tooltip("Nombre exacto de la escena a la que quieres ir")]
    public string nombreEscenaMenu = "MenuPrincipal";

    private bool cambiandoEscena = false;

    void Update()
    {
        // Detecta si se presiona cualquier tecla del teclado, botón de mando o clic del ratón/pantalla táctil
        if ((Input.anyKeyDown || Input.GetMouseButtonDown(0)) && !cambiandoEscena)
        {
            CargarMenu();
        }
    }

    private void CargarMenu()
    {
        cambiandoEscena = true;
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}