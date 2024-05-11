using System.IO;

public class SaveManager<T>
{
    public static bool LoadSave(string saveName, string saveType, SaveData<T> data)
    {
        var savePath = Path.Combine(saveName, saveType);
        bool status = SaveFileManager.LoadFromFile(savePath, out string dataJSON);
        if(!status) return false;

        data.LoadFromJson(dataJSON);
        return true;
    }

    public static bool Save(string saveName, string saveType, SaveData<T> data)
    {
        string dataJSON = data.ToJson();
        var savePath = Path.Combine(saveName, saveType);

        return SaveFileManager.WriteToFile(savePath, dataJSON);
    }
}