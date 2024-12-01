using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
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
    bool isAudio = true;
    private void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Toggle audioChoice = GameObject.Find("Audio").GetComponent<Toggle>();
            if (audioChoice)
            {
                isAudio = audioChoice.isOn;
                transform.Find("Bgm").gameObject.SetActive(isAudio);
            }


        }

    }


}
