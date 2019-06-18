using UnityEngine;
using UnityEngine.UI;
using System;

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

    private void Start()
    {
       score1.text = (String.Format("{0:00.00}/20", GameMaster.gameMaster.getScore(0)));
       score2.text = (String.Format("{0:00.00}/20", GameMaster.gameMaster.getScore(1)));
       score3.text = (String.Format("{0:00.00}/20", (GameMaster.gameMaster.getScore(0)+GameMaster.gameMaster.getScore(1))/2));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GameMaster.gameMaster.End();
    }
}