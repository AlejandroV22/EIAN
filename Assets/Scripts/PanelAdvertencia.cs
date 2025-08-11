using UnityEngine;
using TMPro;

public class PanelAdvertencia : MonoBehaviour
{
    public GameObject panelCompleto;
    public TextMeshProUGUI mensajeTexto;

    public void MostrarMensaje(string mensaje)
    {
        mensajeTexto.text = mensaje;
        panelCompleto.SetActive(true);
    }

    public void Ocultar()
    {
        panelCompleto.SetActive(false);
    }
}
