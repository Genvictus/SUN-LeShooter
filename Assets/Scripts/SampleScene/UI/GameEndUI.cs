using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
    public Text gameEndText;
    public GameObject gameEndObject;

    public void EndGame(string text)
    {
        gameEndText.text = text;
        gameEndObject.SetActive(true);
    }
}
