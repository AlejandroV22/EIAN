using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorQuiz : MonoBehaviour
{
    public BancoDePreguntas banco;
    public TextMeshProUGUI textoPregunta;
    public Button[] botonesRespuesta;
    public TextMeshProUGUI textoContador;
    public GameObject panelResultado;
    public TextMeshProUGUI textoResultado;
    public string nombreTema;

    private List<Pregunta> preguntasSeleccionadas;
    private int indicePreguntaActual = 0;
    private int respuestasCorrectas = 0;

    void Start()
    {
        GenerarQuizAleatorio();
        MostrarPreguntaActual();
    }

    void GenerarQuizAleatorio()
    {
        preguntasSeleccionadas = new List<Pregunta>();

        // Agrupar por tema
        Dictionary<string, List<Pregunta>> preguntasPorTema = new Dictionary<string, List<Pregunta>>();

        foreach (Pregunta p in banco.todasLasPreguntas)
        {
            if (!preguntasPorTema.ContainsKey(p.tema))
                preguntasPorTema[p.tema] = new List<Pregunta>();

            preguntasPorTema[p.tema].Add(p);
        }

        // Seleccionar 2 por tema
        foreach (var tema in preguntasPorTema)
        {
            List<Pregunta> preguntasTema = tema.Value;
            for (int i = 0; i < 2 && i < preguntasTema.Count; i++)
            {
                int rand = Random.Range(0, preguntasTema.Count);
                preguntasSeleccionadas.Add(preguntasTema[rand]);
                preguntasTema.RemoveAt(rand);
            }
        }
    }

    void MostrarPreguntaActual()
    {
        if (indicePreguntaActual >= preguntasSeleccionadas.Count)
        {
            MostrarResultadoFinal();
            return;
        }

        Pregunta actual = preguntasSeleccionadas[indicePreguntaActual];
        textoPregunta.text = actual.textoPregunta;
        textoContador.text = $"Pregunta {indicePreguntaActual + 1}/10";

        for (int i = 0; i < botonesRespuesta.Length; i++)
        {
            botonesRespuesta[i].GetComponentInChildren<TextMeshProUGUI>().text = actual.opciones[i];
            int index = i;
            botonesRespuesta[i].onClick.RemoveAllListeners();
            botonesRespuesta[i].onClick.AddListener(() => SeleccionarRespuesta(index));
        }
    }

    void SeleccionarRespuesta(int seleccion)
    {
        if (seleccion == preguntasSeleccionadas[indicePreguntaActual].indiceRespuestaCorrecta)
            respuestasCorrectas++;

        indicePreguntaActual++;
        MostrarPreguntaActual();
    }

    void MostrarResultadoFinal()
    {
        panelResultado.SetActive(true);
        float porcentaje = (respuestasCorrectas / 10f) * 100f;
        textoResultado.text = $"Respuestas correctas: {respuestasCorrectas}/10\nPorcentaje: {porcentaje}%";
        if (respuestasCorrectas >= 7)
        {
            PlayerPrefs.SetInt("QuizAprobado_" + nombreTema, 1);
            PlayerPrefs.Save();
        }
    }


}
