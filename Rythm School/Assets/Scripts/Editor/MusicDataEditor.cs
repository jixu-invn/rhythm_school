using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;

public class MusicDataEditor : EditorWindow
{
    public MusicData musicData;

    private string dataPath = "/StreamingAssets";

    [MenuItem("Window/Music Data Editor")]
    static void Init()
    {
        MusicDataEditor window = (MusicDataEditor)EditorWindow.GetWindow(typeof(MusicDataEditor));
        window.Show();
    }

    private void OnGUI()
    {
        if (musicData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("musicData");

            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save Data"))
            {
                SaveMusicData();
            }
        }

        if (GUILayout.Button("Load Data"))
        {
            LoadMusicData();
        }
    }

    private void LoadMusicData()
    {
        string FilePath = Application.dataPath + dataPath + "/" + SceneManager.GetActiveScene().name + ".json";
        if (File.Exists(FilePath))
        {
            string DataAsJson = File.ReadAllText(FilePath);
            musicData = JsonUtility.FromJson<MusicData>(DataAsJson);
        }
        else
        {
            musicData = new MusicData();
        }
    }

    private void SaveMusicData()
    {
        string FilePath = Application.dataPath + dataPath + "/" + SceneManager.GetActiveScene().name + ".json";
        string DataAsJson = JsonUtility.ToJson(musicData);
        File.WriteAllText(FilePath, DataAsJson);
    }
}
