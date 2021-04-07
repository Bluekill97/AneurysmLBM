using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FileNameFinder {
    public static string GetName(string PathToFolder, string extensionName) {
        DirectoryInfo dir = new DirectoryInfo(PathToFolder);
        FileInfo[] files = dir.GetFiles();

        //look for cel file -> get name of all other files alse
        FileInfo celFile = default;
        foreach (FileInfo f in files) {
            if (f.Extension == extensionName) {
                celFile = f;
                break;
            }
        }

        string filenames = celFile.Name;

        //remove extension
        var parts = filenames.Split('.');
        filenames = parts[0];
        for (int i = 1; i < parts.Length - 1; i++)
            filenames += "." + parts[i];

        Debug.Log("Filenames: " + filenames);

        return filenames;
    }

}