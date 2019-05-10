using System.Collections.Generic;

[System.Serializable]
public class MusicData
{
    public enum Check { Idle, Fail, Ok };

    public float margin = 0.05f;
    public float superMargin = 0.5f;
    public float clueDuration = 1f;

    public BeatData[] Beats;
    public Mapper[] Mappers;
    private int current = 0;
    private float multiplier = 2f;

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

    public List<BeatData> NeedAClue (float currentTime)
    {
        List<BeatData> beatDatas = new List<BeatData>();
        int i = current;
        BeatData beat = Beats[i++];
        
        while (beat.GetNormalizedTimer() - currentTime < multiplier * clueDuration)
        {
            if (beat.GetClued() == false)
            {
                beatDatas.Add(beat);
            }

            beat = Beats[i++];
        }

        return beatDatas;
    }

    public float ActionTime()
    {
        return GetCurrent().GetNormalizedTimer() - superMargin;
    }

    public float OkTime()
    {
        return GetCurrent().GetNormalizedTimer() - margin;
    }

    public float FailTime()
    {
        return GetCurrent().GetNormalizedTimer() + margin;
    }
}
