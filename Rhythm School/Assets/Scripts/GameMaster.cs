using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gameMaster;

    private Scene currentScene;
    private GameController gameController;
    private MusicData musicData;
    private int GlobalScore;

    private void Awake()
    {
        if(gameMaster == null)
        {
            gameMaster = this;
        }
        else
        {
            if (gameMaster != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene;
        gameController = GameController.gameController;
        LoadData();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        musicData = null;
    }

    private void Update()
    {
        if (currentScene.buildIndex == 0)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Launch();
            }
        }
    }
    
    private void NextScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    private void LoadData()
    {
        string FileName = SceneManager.GetActiveScene().name + ".json";
        string DataPath = Path.Combine(Application.streamingAssetsPath, FileName);
        
        Debug.Log(DataPath);

        if (File.Exists(DataPath))
        {
            string DataAsJson = File.ReadAllText(DataPath);
            if (currentScene.name.StartsWith("level"))
            {
                musicData = JsonUtility.FromJson<MusicData>(DataAsJson);
                gameController.SetMusicData(musicData);
            }
        }
        else
        {
            Debug.Log("There no such file here");
        }
    }

    public void Launch()
    {
        NextScene();
    }

    public void End()
    {
        NextScene();
    }

    public void AddToGlobalScore(int _score)
    {
        if (!currentScene.name.StartsWith("tuto"))
        {
            GlobalScore += _score;
        }
    }
}
