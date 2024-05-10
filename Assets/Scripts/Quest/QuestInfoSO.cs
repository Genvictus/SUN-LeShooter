using UnityEngine;


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

    private void OnValidate()
    {
        #if UNITY_EDITOR
        ID = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}