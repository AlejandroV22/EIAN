using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro; // Importante para usar TextMeshProUGUI

public class BotonVolverAVistaPrincipal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Nombre de la escena a cargar")]
    public string nombreEscena;

    private TextMeshProUGUI texto;
    private int tamañoOriginal;

    void Start()
    {
        
        texto = GetComponentInChildren<TextMeshProUGUI>();
        if (texto != null)
        {
            tamañoOriginal = Mathf.RoundToInt(texto.fontSize);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (texto != null)
            texto.fontSize = tamañoOriginal + 7; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (texto != null)
            texto.fontSize = tamañoOriginal;
    }

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
