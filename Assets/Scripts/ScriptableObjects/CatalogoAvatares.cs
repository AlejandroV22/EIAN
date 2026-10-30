using UnityEngine;

[CreateAssetMenu(fileName = "CatalogoAvatares", menuName = "Juego/Catalogo de Avatares")]
public class CatalogoAvatares : ScriptableObject
{
    public AvatarData[] avatares;     // Avatar 1, 2, 3, 4
    public string[] nombresTemas;
    public int quizzesParaIntermedio = 1;
    public int quizzesParaAvanzado = 3;
}