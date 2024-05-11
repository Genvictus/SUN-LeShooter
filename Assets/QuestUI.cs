using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    private static Dictionary<string, string> questDescriptions = new();
    static Canvas questCanvas;
    static Text questText;
    
    void Start()
    {
        questDescriptions = new();
        questCanvas = GetComponentInChildren<Canvas>();
        questText = questCanvas.GetComponentInChildren<Text>();
        UpdateUI();
    }

    void Update()
    {
    }

    public static void AddDescription(string questID, string description) {
        questDescriptions.Add(questID, description);
        UpdateUI();
    }

    public static void RemoveDescription(string questID) {
        questDescriptions.Remove(questID);
        UpdateUI();
    }

    public static void UpdateDescription(string questID, string description) {
        questDescriptions[questID] = description;
        UpdateUI();
    }

    private static void UpdateUI() {
        if (questDescriptions.Count == 0) {
            questCanvas.enabled = false;
        } else {
            questCanvas.enabled = true;
            questText.text = "Quest\n";
            foreach (string questDescription in questDescriptions.Values)
            {
                questText.text += questDescription + '\n';
            }
        }
    }
}
