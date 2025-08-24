using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ConectarColumnas : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject panelVictoria;

    [Header("Pares correctos (izquierda[i] va con derecha[i])")]
    public List<Button> izquierda;
    public List<Button> derecha;

    private Dictionary<Button, Button> paresCorrectos = new Dictionary<Button, Button>();
    private Dictionary<Button, ColorBlock> coloresOriginales = new Dictionary<Button, ColorBlock>();

    private Button opcionSeleccionadaIzquierda = null;
    private Button opcionSeleccionadaDerecha = null;
    private int paresResueltos = 0;

    void Start()
    {
        if (panelVictoria) panelVictoria.SetActive(false);

        // Construir diccionario y guardar colores originales
        for (int i = 0; i < izquierda.Count; i++)
        {
            paresCorrectos.Add(izquierda[i], derecha[i]);

            coloresOriginales[izquierda[i]] = izquierda[i].colors;
            coloresOriginales[derecha[i]]  = derecha[i].colors;

            int index = i; // evitar el problema de closure
            izquierda[i].onClick.AddListener(() => SeleccionarIzquierda(izquierda[index]));
            derecha[i].onClick.AddListener(() => SeleccionarDerecha(derecha[index]));
        }
    }

    void SeleccionarIzquierda(Button btn)
    {
        opcionSeleccionadaIzquierda = btn;
        VerificarPar();
    }

    void SeleccionarDerecha(Button btn)
    {
        opcionSeleccionadaDerecha = btn;
        VerificarPar();
    }

    void VerificarPar()
    {
        if (opcionSeleccionadaIzquierda == null || opcionSeleccionadaDerecha == null) return;

        
        EventSystem.current?.SetSelectedGameObject(null);

        if (paresCorrectos[opcionSeleccionadaIzquierda] == opcionSeleccionadaDerecha)
        {
            // Correcto
            opcionSeleccionadaIzquierda.interactable = false;
            opcionSeleccionadaDerecha.interactable = false;

            PintarTodosLosEstados(opcionSeleccionadaIzquierda, Color.green);
            PintarTodosLosEstados(opcionSeleccionadaDerecha, Color.green);

            paresResueltos++;
            if (panelVictoria && paresResueltos == paresCorrectos.Count)
                panelVictoria.SetActive(true);
        }
        else
        {
            // Incorrecto: parpadeo rojo en ambos
            StartCoroutine(ParpadeoError(opcionSeleccionadaIzquierda));
            StartCoroutine(ParpadeoError(opcionSeleccionadaDerecha));
        }

        opcionSeleccionadaIzquierda = null;
        opcionSeleccionadaDerecha = null;
    }

    void PintarTodosLosEstados(Button btn, Color color)
    {
        var cb = btn.colors;
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.pressedColor = color;
        cb.selectedColor = color;
        cb.disabledColor = color;
        btn.colors = cb;
    }

    System.Collections.IEnumerator ParpadeoError(Button btn)
    {
        // Guardar original si no está guardado (por seguridad)
        if (!coloresOriginales.ContainsKey(btn))
            coloresOriginales[btn] = btn.colors;

        // Poner todos los estados en rojo
        PintarTodosLosEstados(btn, Color.red);

        yield return new WaitForSeconds(0.4f);

        // Restaurar colores originales
        btn.colors = coloresOriginales[btn];
    }
}
