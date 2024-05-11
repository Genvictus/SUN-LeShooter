using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "QuestInfo", menuName = "Quest/QuestInfo")]
public class QuestInfoSO : ScriptableObject
{
    [SerializeField] public string ID { get; private set; }

    [Header("General")]
    public string displayName;
    [Header("Requirements")]
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Completion")]
    public bool automaticallyComplete;

    [Header("On Completion")]
    public UnityEvent finishingEvent;
    public bool automaticallyStartQuests;
    public QuestInfoSO[] questToStart;


    private void OnValidate()
    {
        #if UNITY_EDITOR
        ID = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}