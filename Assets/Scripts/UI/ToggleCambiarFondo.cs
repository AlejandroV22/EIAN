using UnityEngine;
using UnityEngine.UI;

public class ToggleCambiarFondo: MonoBehaviour
{
    public Toggle toggleCambiarFondo;
    public GameObject imagenPrincipal;
    public GameObject imagenRuta;

    void Start()
    {
        if (toggleCambiarFondo != null)
        {
            toggleCambiarFondo.onValueChanged.AddListener(CambiarImagenFondo);
            CambiarImagenFondo(toggleCambiarFondo.isOn); // actualizar al iniciar
        }
    }

    void CambiarImagenFondo(bool estado)
    {
        imagenPrincipal.SetActive(!estado);
        imagenRuta.SetActive(estado);
    }
}
