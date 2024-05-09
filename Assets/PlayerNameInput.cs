using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_InputField>().text = SavesManager.playerName;
    }

    public void SetPlayerName(string name)
    {
        SavesManager.playerName = name;
        Debug.Log("Player Name : " + SavesManager.playerName);
    }
}
