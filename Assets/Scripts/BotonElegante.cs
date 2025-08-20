using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonElegante : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject subrayado; 
    private Vector3 escalaOriginal;

    void Start()
    {
        if (subrayado != null)
            subrayado.SetActive(false);

        escalaOriginal = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (subrayado != null)
            subrayado.SetActive(true);

        transform.localScale = escalaOriginal * 1.25f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (subrayado != null)
            subrayado.SetActive(false);

        
        transform.localScale = escalaOriginal;
    }
}
