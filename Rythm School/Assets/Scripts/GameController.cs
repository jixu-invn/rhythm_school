﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/*
 * Manage the game engine
 */

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
    public AudioClip Clip;

    public static GameController gameController;

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
        float time = Time.timeSinceLevelLoad - startingTimer;

        if (time > musicData.FailTime())
        {
            HaveFailed(time);
        }
    }

    public MusicData.Check CheckAction(BeatInput i, float time)
    {
        bool ok = true;
        bool hit = false;

        float offsettedTime = time - startingTimer;

        if (offsettedTime < musicData.ActionTime())
        {
            return MusicData.Check.Idle;
        }

        if (offsettedTime < musicData.OkTime())
        {
            return HaveFailed(time);
        }

        foreach(BeatInput bi in musicData.GetCurrent().Inputs)
        {
            if (bi.GetfDone() == false)
            {
                if (!(i.actionType == BeatInput.ActionType.Up && bi.actionType == BeatInput.ActionType.Down))
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
        }

        return ok ? HaveOk(time) : (hit ? MusicData.Check.Idle : HaveFailed(time));
    }

    public MusicData.Check CheckInput(KeyCode k, bool downed, float time)
    {
        if (isPlaying)
        {
            foreach (Mapper m in musicData.Mappers)
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

    private MusicData.Check HaveFailed(float time)
    {
        Debug.Log("Fail : " + musicData.GetCurrent().Timer + " => " + time);

        goNext();
        //play fail animation

        return MusicData.Check.Fail;
    }

    private MusicData.Check HaveOk(float time)
    {
        Debug.Log("Ok : " + musicData.GetCurrent().Timer + " => " + time);

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
        string FileName = SceneManager.GetActiveScene().name + ".json";
        string DataPath = Path.Combine(Application.streamingAssetsPath, FileName);

        Debug.Log(DataPath);
        
        if (File.Exists(DataPath))
        {
            string DataAsJson = File.ReadAllText(DataPath);
            musicData = JsonUtility.FromJson<MusicData>(DataAsJson);
        }
        else
        {
            Debug.Log("There no such file here");
        }
    }
}
