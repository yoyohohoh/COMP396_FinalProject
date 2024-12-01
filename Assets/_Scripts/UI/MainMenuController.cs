using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] public string gameLevel;
    [SerializeField] public GameObject startButton;

    private void Start()
    {

    }

    public void StartGame()
    {
        Destroy(startButton);
    }
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene($"{gameLevel}Level");
    }

    public void InstructionsScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Instructions");
    }

    public void MainMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void ShowOptions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Options");

    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }




}
