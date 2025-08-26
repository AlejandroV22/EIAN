using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardMemoria : MonoBehaviour, IPointerClickHandler
{
    public int cardID; // ID para identificar la pareja
    public GameObject frente;
    public GameObject reverso;

    private bool descubierta = false;
    private bool encontrada = false;

    public void Start()
    {
        Mostrar();
        Invoke(nameof(Ocultar), 4f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (GameManagerMemoria.Instance.IsBloqueado() || descubierta || encontrada)
            return;

        Mostrar();
        GameManagerMemoria.Instance.CartaSeleccionada(this);
    }

    public void Mostrar()
    {
        descubierta = true;
        frente.SetActive(true);
        reverso.SetActive(false);
    }

    public void Ocultar()
    {
        descubierta = false;
        frente.SetActive(false);
        reverso.SetActive(true);
    }

    public void Bloquear()
    {
        encontrada = true; 
    }
}