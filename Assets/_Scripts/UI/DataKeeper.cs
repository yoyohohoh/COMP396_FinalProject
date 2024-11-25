using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class DataKeeper : MonoBehaviour
{
    public static DataKeeper Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    MainMenuController mainMenuController;
    [Header("Level Options")]
    Dropdown dropdownLevelList;
    [SerializeField] string selectedLevel = "Easy";
    [SerializeField] public List<bool> levelAvailability = new List<bool> { true, false, false};
    [SerializeField] public List<string> easyLevelRecord;
    [SerializeField] public List<string> mediumLevelRecord;
    [SerializeField] public List<string> hardLevelRecord;
    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject controllerObject = GameObject.Find("MainMenuController");
        if(controllerObject != null)
        {
            mainMenuController = controllerObject.GetComponent<MainMenuController>();
            if (mainMenuController != null)
            {
                mainMenuController.gameLevel = selectedLevel;
            }
        }

        GameObject dropdowntObject = GameObject.Find("LevelList");
        if (dropdowntObject != null)
        {
            dropdownLevelList = dropdowntObject.GetComponent<Dropdown>();
            if (dropdownLevelList != null)
            {
                for (int i = 0; i < dropdownLevelList.options.Count; i++)
                {
                    if (dropdownLevelList.options[i].text == selectedLevel)
                    {
                        dropdownLevelList.value = i;
                        break;
                    }
                }

                dropdownLevelList.onValueChanged.AddListener(OnDropdownValueChanged);
            }
        }

    }

    void OnDropdownValueChanged(int index)
    {
        selectedLevel = dropdownLevelList.options[index].text;
        mainMenuController.gameLevel = selectedLevel;
    }
    
}
