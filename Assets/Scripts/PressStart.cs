using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class PressStart : MonoBehaviour
{
    [Header("Configuración de Escena")]
    [Tooltip("Nombre exacto de la escena del menú principal")]
    public string nombreEscenaMenu = "MenuPrincipal";

    [Tooltip("Nombre exacto de la escena de selección de avatar")]
    public string nombreEscenaSeleccion = "SeleccionAvatar";

    private bool cambiandoEscena = false;

    void Update()
    {
        Debug.Log("Update corriendo");
        // Detecta si se presiona cualquier tecla del teclado, botón de mando o clic del ratón/pantalla táctil
        if ((Input.anyKeyDown || Input.GetMouseButtonDown(0)) && !cambiandoEscena)
        {
            CargarSiguienteEscena();
        }
    }

    private void CargarSiguienteEscena()
    {
        cambiandoEscena = true;

        bool perfilCreado = PlayerPrefs.GetInt("PerfilCreado", 0) == 1;
        string destino = perfilCreado ? nombreEscenaMenu : nombreEscenaSeleccion;

        SceneManager.LoadScene(destino);
    }

    [ContextMenu("Resetear perfil")]
    private void ResetearPerfil()
    {
        PlayerPrefs.DeleteKey("PerfilCreado");
        PlayerPrefs.Save();
        Debug.Log("Perfil reseteado");
    }
}