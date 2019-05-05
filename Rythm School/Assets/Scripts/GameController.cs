using System.Collections;
using UnityEngine;
using System.IO;

/*
 * Manage the game engine
 */

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
    public AudioClip Clip;
    public string FileName;

    static GameController gameController;

    private AudioSource audioSource;
    private MusicData musicData;
    private float startingTimer = -1;
    private bool isPlaying = false;

    private void Awake()
    {
        if (GameController.gameController == null)
        {
            gameController = this;
        }
        else
        {
            if (GameController.gameController != this)
            {
                Destroy(gameObject);
            }
        }

        if (Clip == null)
        {
            Debug.LogError("No Clip");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Clip;
        LoadData();
        StartLevel();
    }

    private void Update()
    {
        if (isPlaying)
        {
            CheckBeat();
        }
    }

    private void CheckBeat()
    {
        if (Time.timeSinceLevelLoad - startingTimer > musicData.FailTime())
        {
            HaveFailed();
        }
    }

    public MusicData.Check CheckAction(BeatInput i, float time)
    {
        bool ok = true;
        bool hit = false;

        if (time < musicData.ActionTime())
        {
            return MusicData.Check.Idle;
        }

        if (time < musicData.OkTime())
        {
            return HaveFailed();
        }

        foreach(BeatInput bi in musicData.GetCurrent().Inputs)
        {
            if (bi.GetfDone() == false)
            {
                if (bi.Action == i.Action && bi.actionType == i.actionType)
                {
                    bi.Done();
                    hit = true;
                }
                else
                {
                    ok = false;   
                }
            }
        }

        return ok ? HaveOk() : (hit ? MusicData.Check.Idle : HaveFailed());
    }

    public MusicData.Check CheckInput(KeyCode k, bool downed, float time)
    {
        foreach(Mapper m in musicData.Mappers)
        {
            if (m.input == k)
            {
                BeatInput input = new BeatInput
                {
                    Action = m.action,
                    actionType = downed ? BeatInput.ActionType.Down : BeatInput.ActionType.Up
                };

                return CheckAction(input, time);
            }
        }

        return MusicData.Check.Idle;
    }

    private void goNext()
    {
        if (musicData.GetCurrent() != musicData.End())
        {
            musicData.Next();
        }
        else
        {
            isPlaying = false;
        }
    }

    private MusicData.Check HaveFailed()
    {
        goNext();
        //play fail animation
        return MusicData.Check.Fail;
    }

    private MusicData.Check HaveOk()
    {
        goNext();
        //play animation
        return MusicData.Check.Ok;
    }
    
    private void StartLevel()
    {
        audioSource.Play();
        startingTimer = Time.timeSinceLevelLoad;
        isPlaying = true;
    }

    private void LoadData()
    {
        string DataPath = Path.Combine(Application.streamingAssetsPath, FileName);

        Debug.Log(DataPath);

        if (File.Exists(DataPath))
        {
            Debug.Log("Indeed, this file does exist");
            //load data
        }
        else
        {
            Debug.Log("There no such file here");
        }
    }
}
