using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject manuPanel;
    public GameObject creditPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoToGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("InGame_1"); 
    }

    public void OpenCredits()
    {
        print("check");
        creditPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditPanel.SetActive(false);
    }
}
