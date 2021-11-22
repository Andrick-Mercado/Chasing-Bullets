using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject textDisplayPlayOrNot;
    public void PlayGame()
    {
        if(Game_Manager.GetInstance() == null)
        {
            textDisplayPlayOrNot.GetComponent<TextMeshProUGUI>().text = "MUST FINISH CHARACTER FIRST";
            return;
        }
        if (string.IsNullOrEmpty(Game_Manager.GetInstance().getplayerName()))
        {
            textDisplayPlayOrNot.GetComponent<TextMeshProUGUI>().text = "MUST FINISH CHARACTER FIRST";
            return;
        }
        if (string.IsNullOrEmpty(Game_Manager.GetInstance().getplayerColor()))
        {
            textDisplayPlayOrNot.GetComponent<TextMeshProUGUI>().text = "MUST FINISH CHARACTER FIRST";
            return;
        }
        if (string.IsNullOrEmpty(Game_Manager.GetInstance().getplayerDifficulty()))
        {
            textDisplayPlayOrNot.GetComponent<TextMeshProUGUI>().text = "MUST FINISH CHARACTER FIRST";
            return;
        }

        SceneManager.LoadScene(4);
 
    }

    public void RollCharacter()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        SceneManager.LoadScene(2);
    }    

    public void About()
    {
        SceneManager.LoadScene(3);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // set the playmode to stop
#else
         Application.Quit();
#endif
    }
}
