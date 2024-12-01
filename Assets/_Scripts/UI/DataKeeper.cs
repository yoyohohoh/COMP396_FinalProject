using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool isFirstLoad = true;
    [SerializeField] public string player1Name = "Player 1";
    [SerializeField] public string player2Name = "Player 2";
    [SerializeField] string selectedLevel = "Easy";
    [SerializeField] public List<bool> levelAvailability = new List<bool> { true, false, false };
    [SerializeField] public List<string> easyLevelRecord;
    [SerializeField] public List<string> mediumLevelRecord;
    [SerializeField] public List<string> hardLevelRecord;
    bool isMediumAvailable = false;
    bool isHardAvailable = false;
    private void Start()
    {
        player1Name = "Player 1";
        player2Name = "Player 2";
    }
    private void Update()
    {
        levelAvailability = new List<bool> { true, isMediumAvailable, isHardAvailable };
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            player1Name = GameObject.Find("Canvas").transform.Find("Control").transform.Find("Player1").transform.Find("Name").gameObject.GetComponent<InputField>().text;
            player2Name = GameObject.Find("Canvas").transform.Find("Control").transform.Find("Player2").transform.Find("Name").gameObject.GetComponent<InputField>().text;
        }
    }
    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject controllerObject = GameObject.Find("MainMenuController");
        if (controllerObject != null)
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (currentSceneIndex)
        {
            case 3:
                easyLevelRecord = new List<string>();
                break;
            case 4:
                mediumLevelRecord = new List<string>();
                isMediumAvailable = true;
                break;
            case 5:
                hardLevelRecord = new List<string>();
                isHardAvailable = true;
                break;
        }

    }

    void OnDropdownValueChanged(int index)
    {
        selectedLevel = dropdownLevelList.options[index].text;
        mainMenuController.gameLevel = selectedLevel;
    }

}
