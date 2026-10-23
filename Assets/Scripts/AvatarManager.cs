using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class AvatarData
{
    public string nombre;      // "Avatar 1"
    public Sprite principiante;
    public Sprite intermedio;
    public Sprite avanzado;
}

public class AvatarManager : MonoBehaviour
{
    [Header("Referencias")]
    public Image imagenAvatar;
    public TextMeshProUGUI nombreAvatar;
    public TMP_Dropdown dropdownAvatar;

    [Header("Avatares (en el mismo orden que el dropdown)")]
    public AvatarData[] avatares;   // Avatar 1, 2, 3, 4

    [Header("Temas del quiz (en orden)")]
    public string[] nombresTemas;

    [Header("Umbrales de progreso (cantidad de quizzes aprobados)")]
    public int quizzesParaIntermedio = 1;
    public int quizzesParaAvanzado = 3;

    private const string KeyAvatarSeleccionado = "AvatarSeleccionado";

    void Start()
    {
        int guardado = Mathf.Clamp(PlayerPrefs.GetInt(KeyAvatarSeleccionado, 0), 0, avatares.Length - 1);
        dropdownAvatar.value = guardado;
        dropdownAvatar.RefreshShownValue();
        dropdownAvatar.onValueChanged.AddListener(CambiarAvatar);

        ActualizarAvatar();
    }


    public void CambiarAvatar(int indice)
    {
        PlayerPrefs.SetInt(KeyAvatarSeleccionado, indice);
        PlayerPrefs.Save();
        ActualizarAvatar();
    }

    public void ActualizarAvatar()
    {
        if (avatares == null || avatares.Length == 0) return;

        int seleccionado = Mathf.Clamp(PlayerPrefs.GetInt(KeyAvatarSeleccionado, 0), 0, avatares.Length - 1);
        AvatarData avatar = avatares[seleccionado];

        int aprobados = ContarQuizzesAprobados();

        Sprite forma;
        if (aprobados >= quizzesParaAvanzado)
            forma = avatar.avanzado;
        else if (aprobados >= quizzesParaIntermedio)
            forma = avatar.intermedio;
        else
            forma = avatar.principiante;

        imagenAvatar.sprite = forma;
        nombreAvatar.text = avatar.nombre;
    }

    int ContarQuizzesAprobados()
    {
        int aprobados = 0;
        foreach (string tema in nombresTemas)
        {
            if (PlayerPrefs.GetInt("QuizAprobado_" + tema, 0) == 1)
                aprobados++;
        }
        return aprobados;
    }
}