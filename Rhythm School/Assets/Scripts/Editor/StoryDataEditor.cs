using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;

public class StoryDataEditor : EditorWindow
{
    public StoryData StoryData;

    private string dataPath = "/StreamingAssets";
    private Vector2 scrollPos;
    private string suffix = "";

    [MenuItem("Window/Story Data Editor")]
    static void Init()
    {
        StoryDataEditor window = (StoryDataEditor)EditorWindow.GetWindow(typeof(StoryDataEditor));
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        suffix = EditorGUILayout.TextField("Suffixe du fichier", suffix);
        if (StoryData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("StoryData");

            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save Data"))
            {
                SaveStoryData();
            }
        }
        if (suffix.Length > 0)
            if (GUILayout.Button("Load Data"))
            {
                LoadStoryData();
            }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void LoadStoryData()
    {
        string FilePath = Application.dataPath + dataPath + "/" + SceneManager.GetActiveScene().name + "_" + suffix + ".json";
        if (File.Exists(FilePath))
        {
            string DataAsJson = File.ReadAllText(FilePath);
            StoryData = JsonUtility.FromJson<StoryData>(DataAsJson);
        }
        else
        {
            StoryData = new StoryData();
        }
    }

    private void SaveStoryData()
    {
        string FilePath = Application.dataPath + dataPath + "/" + SceneManager.GetActiveScene().name + "_" + suffix + ".json";
        string DataAsJson = JsonUtility.ToJson(StoryData);
        File.WriteAllText(FilePath, DataAsJson);
    }
}
