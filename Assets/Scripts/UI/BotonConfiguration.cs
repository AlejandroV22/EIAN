using UnityEngine;

public class BotonConfiguration : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject panelConfiguration;
    public GameObject settingsButton;

        void Start()
        {
            int quiz1Done = PlayerPrefs.GetInt("QuizAprobado_quiz1", 0);
            Debug.Log("Quiz1: " + PlayerPrefs.GetInt("QuizAprobado_quiz1", 0));
            Debug.Log(quiz1Done);
            if (quiz1Done == 1 && settingsButton != null)
            {
                settingsButton.SetActive(true);
            }
        }
        public void OpenConfiguration()
        {
            if (panelConfiguration != null)
            {
                panelConfiguration.SetActive(true);
            }
            Debug.Log("Configuración abierta");
        }
}
