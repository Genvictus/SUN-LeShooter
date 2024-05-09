using System;
using System.IO;
using UnityEngine;

public class SaveFileManager
{
    private static readonly string SAVEPATH = Path.Combine(Application.persistentDataPath, "Saves");

    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        CreateDirectory();
        var fullPath = Path.Combine(SAVEPATH, a_FileName);

        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            Debug.Log($"Successfully saved file at {fullPath}");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error writing file to {fullPath}: {e}");
        }

        return false;
    }

    public static bool LoadFromFile(string a_FileName, out string content)
    {
        var fullPath = Path.Combine(SAVEPATH, a_FileName);

        try
        {
            content = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error reading file {fullPath}: {e}");
        }

        content = null;
        return false;
    }

    public static bool CreateDirectory(string a_DirPath = null)
    {
        string fullPath = SAVEPATH;
        if (a_DirPath is not null)
        {
            fullPath = Path.Combine(fullPath, a_DirPath);
        }

        // create the directory
        try
        {
            Directory.CreateDirectory(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"Unable to create directory, try asking for permission: {e}");
            return false;
        }
    }

    public static bool RenameDirectory(string old_DirName, string new_DirName)
    {
        var old_FullDir = Path.Combine(SAVEPATH, old_DirName);
        var new_FullDir = Path.Combine(SAVEPATH, new_DirName);
        try
        {
            Directory.Move(old_FullDir, new_FullDir);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error renaming directory: {e}");
        }

        return false;
    }
}