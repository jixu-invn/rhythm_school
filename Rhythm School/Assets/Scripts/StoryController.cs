using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource), typeof(AnimationManager))]
public class StoryController : MonoBehaviour
{
    public static StoryController storyController;
    private StoryData storyData;
    private AnimationManager animationManager;
    private bool isLoaded = false;
    private string suffix = "";
    private float currentGrade;
    private float oldGrade;
    private string currentText = "";
    public float delay = 0.05f;
    private bool talking = false;
    public Text textBox;

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
       
        if (oldGrade>=50){
            suffix = "OK";
        }
        else {
            suffix = "MEH";
        }

        if (currentGrade >= 80){
            suffix += "PER";
        }
        else if(currentGrade>=50){
            suffix += "OK";
        }
        else if(currentGrade >= 20){
            suffix += "MEH";
        }
        else{
            suffix += "FAIL";
        }

        string FileName = SceneManager.GetActiveScene().name + "_" + suffix + ".json";
        Debug.Log(FileName);
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
       
    }

    private void Update()
    {
        if (!talking && ((storyData.Dialog.Length) != storyData.GetIndex()))
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StartCoroutine(PrintDialog());
                // Gerer emotions
            }
        }
        else if (!talking && (storyData.Dialog.Length) == storyData.GetIndex())
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                GameMaster.gameMaster.End();
            }
        }
    }

    IEnumerator PrintDialog()
    {
        talking = true;
        for(int i = 0; i < storyData.GetCurrent().Phrase.Length + 1; i++)
        {
            currentText = storyData.GetCurrent().Phrase.Substring(0, i);
            textBox.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        storyData.Next();
        talking = false;
    }

}
