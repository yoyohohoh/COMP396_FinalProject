using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Dropdown dropdownLevelList;
    [SerializeField] GameObject disableLevelNoti;
    DataKeeper dataKeeper;
    private List<bool> optionAvailability;

    void Start()
    {
        dropdownLevelList = GameObject.Find("LevelList").GetComponent<Dropdown>();
        if(dropdownLevelList != null)
        {
            dataKeeper = GameObject.Find("DataKeeper").GetComponent<DataKeeper>();
            if(dataKeeper != null)
            {
                optionAvailability = dataKeeper.levelAvailability;
            }
            dropdownLevelList.onValueChanged.AddListener(OnDropdownValueChanged);
            StartCoroutine(WaitForDropdownInitialization());
        }
    }

    private IEnumerator<WaitForSeconds> WaitForDropdownInitialization()
    {
        yield return null;

        UpdateDropdownOptions();
    }

    void UpdateDropdownOptions()
    {
        for (int i = 0; i < dropdownLevelList.options.Count; i++)
        {
            var optionsComponent = dropdownLevelList.transform.Find("Dropdown List/Viewport/Content/Item " + i + "/Item Label");
            if(optionsComponent != null){
                var textComponent = dropdownLevelList.transform.Find("Dropdown List/Viewport/Content/Item " + i + "/Item Label").GetComponent<Text>();

                if (textComponent != null)
                {
                    textComponent.color = optionAvailability[i] ? Color.black : Color.gray;
                }
            }
        }
    }

    void OnDropdownValueChanged(int index)
    {
        if (!optionAvailability[index])
        {
            Debug.Log("Option is disabled!");
            if (disableLevelNoti != null)
            {
                disableLevelNoti.SetActive(true);
                Invoke("DestroyNoti", 2.0f);
            }
            dropdownLevelList.value = optionAvailability.FindIndex(isAvailable => isAvailable);
            return;
        }

        Debug.Log("Selected option: " + dropdownLevelList.options[index].text);
    }

    void DestroyNoti() 
    {
        disableLevelNoti.SetActive(false);
    }
}
