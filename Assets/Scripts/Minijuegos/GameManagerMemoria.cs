using System.Collections;
using UnityEngine;

public class GameManagerMemoria : MonoBehaviour
{
    public static GameManagerMemoria Instance;

    private CardMemoria primeraCarta = null;
    private CardMemoria segundaCarta = null;
    private bool bloqueado = false;
    public bool IsBloqueado() => bloqueado;

    private int parejasEncontradas = 0;
    public int totalParejas = 4;

    [Header("UI")]
    public GameObject panelVictoria;

    [Header("Cartas")]
    public Transform contenedorCartas; // 🔹 Padre que contiene todas las cartas

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        MezclarCartas();
    }

    private void MezclarCartas()
    {
        int total = contenedorCartas.childCount;
        for (int i = 0; i < total; i++)
        {
            // Elegir un índice aleatorio
            int randomIndex = Random.Range(0, total);
            // Cambiar el orden en la jerarquía
            contenedorCartas.GetChild(randomIndex).SetSiblingIndex(i);
        }
    }

    public void CartaSeleccionada(CardMemoria carta)
    {
        if (bloqueado) return;

        if (primeraCarta == null)
        {
            primeraCarta = carta;
        }
        else if (segundaCarta == null && carta != primeraCarta)
        {
            segundaCarta = carta;
            bloqueado = true;
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
        bloqueado = false;
    }

    private void Victoria()
    {
        Debug.Log("¡Has encontrado todas las parejas!");
        if (panelVictoria != null)
            panelVictoria.SetActive(true);
    }
}