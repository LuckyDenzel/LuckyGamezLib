using System.IO;
using UnityEngine;

namespace LuckyGamezLib {

    public static class SaveSystem {

        // Save the data you want to save in a file
        public static void Save<T>(T data, string fileName) {
            string json = JsonUtility.ToJson(data);
            string path = GetPath(fileName);

            File.WriteAllText(path, json);
        }

        // Load the data you've saved from a file
        public static T LoadSavedDataFromFile<T>(string fileName) {
            string path = GetPath(fileName);

            if (File.Exists(path)) {
                string json = File.ReadAllText(path);

                return JsonUtility.FromJson<T>(json);
            }

            return default;
        }

        // Get the path from a file by name
        public static string GetPath(string fileName) {
            return Path.Combine(Application.persistentDataPath, fileName + ".json");
        }

    }
}