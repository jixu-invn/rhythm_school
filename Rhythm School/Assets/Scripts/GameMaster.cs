using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gameMaster;

    private Scene currentScene;
    private GameController gameController;
    private MusicData musicData;
    private float GlobalScore = 0;
    private float oldGrade = 100;
    private float currentGrade = 100;
    private float nbLevel = 0;
    private float[] scores = new float[2];


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
    }

    public void Launch()
    {
        NextScene();
    }

    public void End()
    {
        NextScene();
    }

    public void AddToGlobalScore(float _score)
    {
        if (!currentScene.name.StartsWith("tuto"))
        {
            scores[(int)nbLevel] = _score;
            oldGrade = currentGrade;
            GlobalScore += _score;
            nbLevel++;
            currentGrade = GlobalScore / nbLevel;
        }
    }

    public float getScore(int i)
    {
        return scores[i];
    }

    public float getOldGrade()
    {
        return oldGrade;
    }

    public float getCurrentGrade()
    {
        return currentGrade;
    }
}
