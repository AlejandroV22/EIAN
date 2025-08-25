using System.Collections;
using UnityEngine;

public class GameManagerMemoria : MonoBehaviour
{
    public static GameManagerMemoria Instance;

    private CardMemoria primeraCarta = null;
    private CardMemoria segundaCarta = null;

    private int parejasEncontradas = 0;
    public int totalParejas = 4;

    [Header("UI")]
    public GameObject panelVictoria;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CartaSeleccionada(CardMemoria carta)
    {
        if (primeraCarta == null)
        {
            primeraCarta = carta;
        }
        else if (segundaCarta == null && carta != primeraCarta)
        {
            segundaCarta = carta;
            StartCoroutine(VerificarPareja());
        }
    }

    private IEnumerator VerificarPareja()
    {
        yield return new WaitForSeconds(1f);

        if (primeraCarta.cardID == segundaCarta.cardID)
        {
            primeraCarta.Bloquear();
            segundaCarta.Bloquear();

            parejasEncontradas++;
            if (parejasEncontradas >= totalParejas)
            {
                Victoria();
            }
        }
        else
        {
            primeraCarta.Ocultar();
            segundaCarta.Ocultar();
        }

        primeraCarta = null;
        segundaCarta = null;
    }

    private void Victoria()
    {
        Debug.Log("¡Has encontrado todas las parejas!");
        if (panelVictoria != null)
            panelVictoria.SetActive(true);
    }
}
