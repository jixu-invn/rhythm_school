using UnityEngine;
using System.Collections.Generic;

/*
 * Manage the game engine
 */

[RequireComponent(typeof(AudioSource),typeof(AnimationManager),typeof(PlayerController))]
public class GameController : MonoBehaviour
{
    public static GameController gameController;

    public AudioClip Clip;

    private PlayerController playerController;
    private AudioSource audioSource;
    private MusicData musicData;
    private float startingTimer = -1;
    private bool isPlaying = false;
    private bool isLoaded = false;
    private bool initilised = false;

    private AnimationManager animationManager;

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

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Clip;
    }

    private void Start()
    {
        playerController = PlayerController.playerController;
        animationManager = AnimationManager.animationManager;
    }

    private void Update()
    {
        if (isLoaded)
        {
            SetClues();
            if (Time.timeSinceLevelLoad - startingTimer > Clip.length)
            {
                playerController.End();
            }
        }
        if (isPlaying)
        {
            CheckBeat();
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
                }
            }
        }

        foreach(BeatInput bi in musicData.GetCurrent().Inputs)
        {
            if (bi.GetfDone() == false)
            {
                ok = false;
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

    private void CheckBeat()
    {
        float time = Time.timeSinceLevelLoad - startingTimer;

        if (time > musicData.FailTime())
        {
            HaveFailed(time);
        }
    }

    private void goNext()
    {
        if (musicData.GetCurrent() != musicData.End())
        {
            musicData.Next();
            InitAnim();
        }
        else
        {
            isPlaying = false;
        }
    }

    private MusicData.Check HaveFailed(float time)
    {        
        foreach(StateMachine s in musicData.GetCurrent().stateMachines)
        {
            s.SetFail();
            animationManager.Play(s);
        }

        goNext();

        return MusicData.Check.Fail;
    }

    private MusicData.Check HaveOk(float time)
    {        
        foreach (StateMachine s in musicData.GetCurrent().stateMachines)
        {
            s.SetOk();
            animationManager.Play(s);
        }

        goNext();

        return MusicData.Check.Ok;
    }
    
    private void StartLevel()
    {
        startingTimer = Time.timeSinceLevelLoad+4;
        audioSource.PlayDelayed(4);
        Invoke("Launch", 4);
        isLoaded = true;
    }
    

    private void Launch()
    {
        isPlaying = true;
        InitAnim();
    }

    private void InitAnim()
    {
        foreach (StateMachine s in musicData.GetCurrent().stateMachines)
        {
            if (s.NeedInit)
                animationManager.Init(s);
        }
    }

    private void SetClues()
    {
        List<BeatData> beatDatas = musicData.NeedAClue(Time.timeSinceLevelLoad - startingTimer);

        foreach (BeatData bd in beatDatas)
        {
            foreach (StateMachine sm in bd.stateMachines)
            {
                if (animationManager.InitClue(sm, bd.GetNormalizedTimer() - (Time.timeSinceLevelLoad - startingTimer), musicData.clueDuration))
                {
                    sm.HasBeenClued();
                }
            }

            bool ok = true;

            foreach(StateMachine sm in bd.stateMachines)
            {
                if (sm.GetClued() == false)
                    ok = false;
            }

            if (ok)
            {
                bd.HasBeenClued();
            }
                
        }
    }

    public void SetMusicData(MusicData _musicData)
    {
        if (initilised == false)
        {
            musicData = _musicData;
            StartLevel();
            initilised = true;
        }
    }
}
