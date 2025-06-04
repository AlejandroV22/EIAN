using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextoInteractivo : MonoBehaviour, IPointerClickHandler
{
    public Camera cam;
    public TextMeshProUGUI texto;

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(texto, eventData.position, cam);
        if (linkIndex != -1)
        {
            var linkInfo = texto.textInfo.linkInfo[linkIndex];
            Debug.Log("Click en link con ID: " + linkInfo.GetLinkID());
        }
    }
}
