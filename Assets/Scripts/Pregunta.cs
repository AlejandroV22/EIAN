using System;

[Serializable]
public class Pregunta
{
    public string tema;                  // Ej: "puente", "tracto", etc.
    public string textoPregunta;         // El texto de la pregunta
    public string[] opciones;            // Las 4 opciones
    public int indiceRespuestaCorrecta;  // 0, 1, 2 o 3
}
