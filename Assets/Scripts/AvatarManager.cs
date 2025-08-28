using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarManager : MonoBehaviour
{
    [Header("Referencias del Avatar")]
    public Image imagenAvatar;
    public TextMeshProUGUI nombreAvatar;

    [Header("Avatares por progreso")]
    public Sprite[] imagenesAvatares;  // imágenes diferentes
    public string[] nombresAvatares;   // nombres diferentes

    void Start()
    {
        ActualizarAvatar();
    }

    public void ActualizarAvatar()
    {
        
        int quizzesAprobados = PlayerPrefs.GetInt("QuizAprobado", 0);

        int index = Mathf.Clamp(quizzesAprobados, 0, imagenesAvatares.Length - 1);

        imagenAvatar.sprite = imagenesAvatares[index];
        nombreAvatar.text = nombresAvatares[index];
    }
}
