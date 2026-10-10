using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class DragImage : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private const string NOMBRE_BOTON = "Centrar";

    private RectTransform rectTransform;
    private Vector2 lastMousePosition;
    private Button botonCentrar;

    [Header("Opciones de centrado")]
    public Vector3 defaultScale = Vector3.one;
    public bool isDraggable = true;
    public float animationDuration = 0.4f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        botonCentrar = BuscarBoton();

        if (botonCentrar != null)
            botonCentrar.onClick.AddListener(CenterImage);
    }

    void OnDestroy()
    {
        // Evita dejar un listener apuntando a un objeto destruido
        if (botonCentrar != null)
            botonCentrar.onClick.RemoveListener(CenterImage);
    }

    private Button BuscarBoton()
    {
        Button[] botones = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Button b in botones)
        {
            if (b.gameObject.name == NOMBRE_BOTON)
                return b;
        }

        Debug.LogWarning($"[DragImage] No se encontró ningún Button llamado '{NOMBRE_BOTON}' en la escena.", this);
        return null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDraggable)
        {
            lastMousePosition = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            Vector2 delta = eventData.position - lastMousePosition;
            rectTransform.anchoredPosition += delta;
            lastMousePosition = eventData.position;
        }
    }

    public void CenterImage()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateToCenter(Vector2.zero, defaultScale));
    }

    IEnumerator AnimateToCenter(Vector2 targetPos, Vector3 targetScale)
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector3 startScale = rectTransform.localScale;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / animationDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, smoothT);
            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, smoothT);

            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;
        rectTransform.localScale = targetScale;
    }
}