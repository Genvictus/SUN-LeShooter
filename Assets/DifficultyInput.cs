using System.Collections;
using System.Collections.Generic;
using Nightmare;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_Dropdown>().value = DifficultyManager.difficulty;
    }
}
