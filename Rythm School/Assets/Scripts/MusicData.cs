﻿[System.Serializable]
public class MusicData
{
    public enum Check { Idle, Fail, Ok };

    public float margin = 0.05f;
    public float superMargin = 0.5f;  

    public BeatData[] Beats;
    public Mapper[] Mappers;
    private int current = 0;

    public int GetIndex()
    {
        return current;
    }
    
    public BeatData GetCurrent()
    {
        return Beats[current];
    }

    public BeatData Next()
    {
        current++;
        return GetCurrent();
    }

    public BeatData End()
    {
        return Beats[Beats.Length - 1];
    }

    public float GetPreviousBeatTime()
    {
        return GetCurrent().Timer / 1000f - (2 * superMargin);
    }

    public float ActionTime()
    {
        return GetCurrent().Timer / 1000f - superMargin;
    }

    public float OkTime()
    {
        return GetCurrent().Timer / 1000f - margin;
    }

    public float FailTime()
    {
        return GetCurrent().Timer / 1000f + margin;
    }
}
