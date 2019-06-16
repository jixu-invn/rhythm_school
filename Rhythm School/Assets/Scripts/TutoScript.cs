using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TutoScript : MonoBehaviour
{
    public Text text;
    public string[] List;
    public string filename;
    
    private int index = 0;
    private MusicData musicData;

    private void Start()
    {
        LoadData();

        if (index < List.Length)
            text.text = List[index++];
    }

    private void Update()
    {
        string s = "";

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (index < List.Length)
            {
                s = List[index];
                for (int j = 0; j < musicData.Mappers.Length; ++j)
                {
                    s = s.Replace("{" + j + "}", musicData.Mappers[j].input.ToString());
                }
                text.text = s;
                index++;
            }
            else
                GameMaster.gameMaster.End();
        }
    }

    private void LoadData()
    {
        string FileName = filename + ".json";
        string DataPath = Path.Combine(Application.streamingAssetsPath, FileName);
        
        if (File.Exists(DataPath))
        {
            string DataAsJson = File.ReadAllText(DataPath);
            musicData = JsonUtility.FromJson<MusicData>(DataAsJson);
        }
    }
}
