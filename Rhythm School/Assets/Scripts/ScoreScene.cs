using UnityEngine;
using UnityEngine.UI;

// UI.Text.text example
//
// A Space keypress changes the message shown on the screen.
// Two messages are used.
//
// Inside Awake a Canvas and Text are created.

public class ScoreScene : MonoBehaviour
{
    public Text score1;
    public Text score2;
    public Text score3;

    void Awake()
    {
      
    }

    void Update()
    {
       score1.text = (GameMaster.gameMaster.getScore(0)/5) + "/20";
       score2.text = (GameMaster.gameMaster.getScore(1)/5) + "/20";
       score3.text = ((GameMaster.gameMaster.getScore(0)+GameMaster.gameMaster.getScore(1))/10) + "/20";
    }
}