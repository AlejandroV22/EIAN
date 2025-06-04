using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeslizarPanel : MonoBehaviour
{
    public RectTransform panel; 
    public Button botonCerrar;
    public Button botonAbrir;

    public float duracion = 0.3f;
    public float desplazamientoX = -300f; // cuánto se moverá el panel hacia la izquierda

    private Vector2 posicionInicial;
    private Vector2 posicionEscondida;
    private bool estaAbierto = true;

    void Start()
    {
        if (panel == null) return;

        posicionInicial = panel.anchoredPosition;
        posicionEscondida = posicionInicial + new Vector2(desplazamientoX, 0);

        botonCerrar.onClick.AddListener(CerrarPanel);
        botonAbrir.onClick.AddListener(AbrirPanel);

        // Asegúrate que al inicio el panel está abierto y el botón de abrir oculto
        botonAbrir.gameObject.SetActive(false);
    }
  

    void CerrarPanel()
    {
        StopAllCoroutines();
        StartCoroutine(MoverPanel(posicionEscondida));
        botonAbrir.gameObject.SetActive(true);
        estaAbierto = false;
    }

    void AbrirPanel()
    {
        StopAllCoroutines();
        StartCoroutine(MoverPanel(posicionInicial));
        botonAbrir.gameObject.SetActive(false);
        estaAbierto = true;
    }

    IEnumerator MoverPanel(Vector2 destino)
    {
        Vector2 origen = panel.anchoredPosition;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            panel.anchoredPosition = Vector2.Lerp(origen, destino, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        panel.anchoredPosition = destino;
    }
}
