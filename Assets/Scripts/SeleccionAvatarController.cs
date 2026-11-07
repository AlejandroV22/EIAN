using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SeleccionAvatarController : MonoBehaviour
{
    [Header("Datos compartidos")]
    public CatalogoAvatares catalogo;

    [Header("Panel 1: Avatar (siempre visible)")]
    public GameObject panelAvatar;
    public Image imagenPreview;
    public Button botonAnterior;
    public Button botonSiguiente;
    public Button botonElegir;

    [Header("Panel 2: Nombre")]
    public GameObject panelNombre;
    public TMP_InputField inputNombre;
    public Button botonConfirmar;

    [Header("Configuración")]
    public int maxCaracteres = 12;
    public string escenaSiguiente = "MenuPrincipal";

    private int indice;
    private bool eligiendoNombre = false;

    void Start()
    {
        indice = Mathf.Clamp(PlayerPrefs.GetInt("AvatarSeleccionado", 0), 0, catalogo.avatares.Length - 1);

        inputNombre.characterLimit = maxCaracteres;

        botonAnterior.onClick.AddListener(() => Cambiar(-1));
        botonSiguiente.onClick.AddListener(() => Cambiar(1));
        botonElegir.onClick.AddListener(MostrarPanelNombre);
        botonConfirmar.onClick.AddListener(Confirmar);

        panelAvatar.SetActive(true);
        panelNombre.SetActive(false);
        MostrarAvatar();
    }

    void Cambiar(int delta)
    {
        if (eligiendoNombre) return;

        int total = catalogo.avatares.Length;
        indice = (indice + delta + total) % total;
        MostrarAvatar();
    }

    void MostrarAvatar()
    {
        AvatarData avatar = catalogo.avatares[indice];
        imagenPreview.sprite = avatar.principiante;
    }

    void MostrarPanelNombre()
    {
        eligiendoNombre = true;
        botonAnterior.interactable = false;
        botonSiguiente.interactable = false;
        botonElegir.interactable = false;
        panelNombre.SetActive(true);

        inputNombre.text = catalogo.avatares[indice].nombreDefecto;
        inputNombre.Select();
        inputNombre.ActivateInputField();
    }

    void Confirmar()
    {
        string nombre = inputNombre.text.Trim();

        if (string.IsNullOrEmpty(nombre))
            nombre = catalogo.avatares[indice].nombreDefecto;

        PlayerPrefs.SetInt("AvatarSeleccionado", indice);
        PlayerPrefs.SetString("NombreJugador", nombre);
        PlayerPrefs.SetInt("PerfilCreado", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(escenaSiguiente);
    }
}