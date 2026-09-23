using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonElegante : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Vector3 escalaOriginal;

    void Start()
    {

        escalaOriginal = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        transform.localScale = escalaOriginal * 1.37f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        
        transform.localScale = escalaOriginal;
    }
}
