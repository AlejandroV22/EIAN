using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BancoDePreguntas", menuName = "Quiz/BancoDePreguntas")]
public class BancoDePreguntas : ScriptableObject
{
    public List<Pregunta> todasLasPreguntas;
}
