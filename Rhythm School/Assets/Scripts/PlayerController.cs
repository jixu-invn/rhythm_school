using UnityEngine;

/*
 * Manage player's input (and maybe her/his datas)
 */

[RequireComponent(typeof(GameController))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController playerController;

    private int score;
    private float groove;
    private MusicData.Check check;

    private GameController gameController;

    private void Awake()
    {
        if (playerController == null)
        {
            playerController = this;
        }
        else
        {
            if (playerController != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    { 
        gameController = GameController.gameController;
      
        score = 0;
        groove = 1;
    }

    private void OnGUI()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        check = MusicData.Check.Idle;
        Event e = Event.current;

        if (e.isKey && e.keyCode != KeyCode.None)
        {
            if(Input.anyKeyDown && e.type == EventType.KeyDown)
            {
                check = gameController.CheckInput(e.keyCode, true, Time.timeSinceLevelLoad);
            }
            if (e.type == EventType.KeyUp)
            {
                check = gameController.CheckInput(e.keyCode, false, Time.timeSinceLevelLoad);
            }
        }

        score += check == MusicData.Check.Ok ? 1 : 0;
    }
    
    public void End(MusicData musicData)
    {
        GameMaster.gameMaster.AddToGlobalScore(score / musicData.Beats.Length * 20);
        GameMaster.gameMaster.End();
    }
}
