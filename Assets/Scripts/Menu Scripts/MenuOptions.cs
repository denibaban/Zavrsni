using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    public string key;
    void Start()
    {
        CheckDefaults();
        if(this.gameObject.GetComponent<InputField>())
            this.gameObject.GetComponent<InputField>().text = PlayerPrefs.GetString(this.gameObject.GetComponent<MenuOptions>().GetKey());
    }

    public void SaveAll()
    {
        GameObject[] KVPairs = GameObject.FindGameObjectsWithTag("OptionKV");
        foreach (GameObject item in KVPairs)
        {
            PlayerPrefs.SetString(item.GetComponent<MenuOptions>().GetKey(), item.GetComponent<MenuOptions>().GetValue());
        }
        PlayerPrefs.Save();
    }

    public string GetKey()
    {
        return key;
    }

    public string GetValue()
    {
        return this.gameObject.GetComponent<InputField>().text;
    }

    void CheckDefaults()
    {
        if (!PlayerPrefs.HasKey("MultiplayerName")) PlayerPrefs.SetString("MultiplayerName", "Default");
        PlayerPrefs.Save();
    }
}
