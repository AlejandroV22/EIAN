using UnityEngine;
using UnityEngine.SceneManagement;

public class Arranque : MonoBehaviour
{
    public string escenaSeleccion = "SeleccionAvatar";
    public string escenaMenu = "MenuPrincipal";

    void Start()
    {
        bool yaCreado = PlayerPrefs.GetInt("PerfilCreado", 0) == 1;
        SceneManager.LoadScene(yaCreado ? escenaMenu : escenaSeleccion);
    }
    [ContextMenu("Resetear perfil")]
    void ResetearPerfil()
    {
        PlayerPrefs.DeleteKey("PerfilCreado");
        PlayerPrefs.Save();
        Debug.Log("Perfil reseteado");
    }
}