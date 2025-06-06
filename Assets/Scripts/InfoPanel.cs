using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InfoPanel : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject panel;
    public TextMeshProUGUI textoInformacion;
    public GameObject miniaturaRecorte3;

    private Camera mainCamera;
void Update()
{
    textoInformacion.ForceMeshUpdate();
    int linkIndex = TMP_TextUtilities.FindIntersectingLink(textoInformacion, Input.mousePosition, null);

    if (linkIndex != -1)
    {
        TMP_LinkInfo linkInfo = textoInformacion.textInfo.linkInfo[linkIndex];
        string linkID = linkInfo.GetLinkID();

        if (linkID == "Recorte3" && miniaturaRecorte3 != null)
        {
            miniaturaRecorte3.SetActive(true);
        }
        else if (miniaturaRecorte3 != null)
        {
            miniaturaRecorte3.SetActive(false);
        }
    }
    else if (miniaturaRecorte3 != null)
    {
        miniaturaRecorte3.SetActive(false);
    }

    // Detectar click y cargar escena (puedes mantener esta parte también aquí)
    if (Input.GetMouseButtonDown(0))
    {
        if (linkIndex != -1)
        {
            string sceneName = textoInformacion.textInfo.linkInfo[linkIndex].GetLinkID();
            Debug.Log("Cargando escena: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }
}


    void Start()
    {
        if (panel != null)
            panel.SetActive(false);

        if (miniaturaRecorte3 != null)
            miniaturaRecorte3.SetActive(false);

        mainCamera = Camera.main;
    }

    public void MostrarInformacion(string info)
    {
        if (panel != null && textoInformacion != null)
        {
            panel.SetActive(true);
            textoInformacion.text = info;
        }
        else
        {
            Debug.LogWarning("Panel o Texto no asignado en InfoPanel.");
        }
    }

    public void Cerrar()
    {
        if (panel != null)
            panel.SetActive(false);
    }



}
