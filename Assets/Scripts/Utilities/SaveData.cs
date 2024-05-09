using UnityEngine;

[System.Serializable]
public abstract class SaveData<T>
{
    public virtual string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public virtual void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

/// <summary>
/// Each class that is going to be saved has to implement the interface
/// to load the data into the class
/// </summary>
public interface ISaveable<T>
{
    void PopulateSaveData(SaveData<T> a_SaveData);
    void LoadFromSaveData(SaveData<T> a_SaveData);
}