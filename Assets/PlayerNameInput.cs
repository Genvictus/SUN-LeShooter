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
        GetComponent<TMP_InputField>().text = SavesHelper.playerName;
    }

    public void SetPlayerName(string name)
    {
        SavesHelper.SetPlayerName(name);
        Debug.Log("Player Name : " + SavesHelper.playerName);
    }
}
