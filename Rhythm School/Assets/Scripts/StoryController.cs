using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(EmotionManager))]
public class StoryController : MonoBehaviour
{
    public AudioClip typing1;
    public AudioClip typing2;
    public AudioClip typing3;
    public static StoryController storyController;
    private StoryData storyData;
    private AnimationManager animationManager;
    private EmotionManager emotionManager;
    private bool isLoaded = false;
    private string suffix = "";
    private float currentGrade;
    private float oldGrade;
    private string currentText = "";
    public float delay = 0.02f;
    private bool talking = false;
    public Text textBox;
    public Text NameBox;
    private Coroutine b;

    private GameController gameController;

    private void Awake()
    {
        if (StoryController.storyController == null)
        {
            storyController = this;
        }
        else
        {
            if (StoryController.storyController != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start(){
        currentGrade = GameMaster.gameMaster.getCurrentGrade();
        oldGrade = GameMaster.gameMaster.getOldGrade();
       
        if (oldGrade>=10){
            suffix = "OK";
        }
        else {
            suffix = "MEH";
        }

        if (currentGrade >= 15){
            suffix += "PER";
        }
        else if(currentGrade>=10){
            suffix += "OK";
        }
        else if(currentGrade >= 4){
            suffix += "MEH";
        }
        else{
            suffix += "FAIL";
        }

        string FileName = SceneManager.GetActiveScene().name + "_" + suffix + ".json";
        string DataPath = Path.Combine(Application.streamingAssetsPath, FileName);
       
        
        if (File.Exists(DataPath))
        {
            string DataAsJson = File.ReadAllText(DataPath);
            storyData = JsonUtility.FromJson<StoryData>(DataAsJson);
        }
        else
        {
            Debug.Log("There is no such file here");
        }
        animationManager = AnimationManager.animationManager;
        emotionManager = EmotionManager.emotionManager;

        b = StartCoroutine(PrintDialog());
        NameBox.text = storyData.GetCurrent().CurrentCharacter.ToString();
        emotionManager.playAnimation(storyData.GetCurrent());
    }

    private void Update()
    {
        if (!talking && ((storyData.Dialog.Length) != storyData.GetIndex()))
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                b = StartCoroutine(PrintDialog());
                NameBox.text = storyData.GetCurrent().CurrentCharacter.ToString();
                emotionManager.playAnimation(storyData.GetCurrent());
            }
        }
        else if (!talking && (storyData.Dialog.Length) == storyData.GetIndex())
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                GameMaster.gameMaster.End();
            }
        }
        else if (talking && (Input.GetKeyUp(KeyCode.Space)))
        {
            StopCoroutine(b);
            textBox.text = storyData.GetCurrent().Phrase;
            storyData.Next();
            talking = false;
        }
    }

    IEnumerator PrintDialog()
    {
        talking = true;
        for(int i = 0; i < storyData.GetCurrent().Phrase.Length + 1; i++)
        {
            if ((i % 3) == 0) AudioSource.PlayClipAtPoint(typing1, transform.position);
            else if ((i%5) == 0) AudioSource.PlayClipAtPoint(typing2, transform.position);
            else if ((i%7) == 0) AudioSource.PlayClipAtPoint(typing3, transform.position);
            currentText = storyData.GetCurrent().Phrase.Substring(0, i);
            textBox.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        storyData.Next();
        talking = false;
    }

}
