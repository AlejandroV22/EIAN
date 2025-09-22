using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZonaInteractiva2 : MonoBehaviour, IPointerClickHandler
{
   

    public string infoTexto;
    public InfoPanel panel;

    [Header("Tracking")]
    public string zonaID;
    public string temaID;




    public void OnPointerClick(PointerEventData eventData)
    {
        panel.MostrarInformacion(infoTexto);
        Debug.Log("Clic detectado en: " + gameObject.name);
        if (!string.IsNullOrEmpty(zonaID))
        {
            PlayerPrefs.SetInt("ZonaVisitada_" + zonaID, 1); // marca como visitada
            PlayerPrefs.Save();
            Debug.Log("Zona marcada como visitada: " + zonaID);
        }
    }

    
    public bool FueVisitada()
    {
        return PlayerPrefs.GetInt("ZonaVisitada_" + zonaID, 0) == 1;
    }
}
