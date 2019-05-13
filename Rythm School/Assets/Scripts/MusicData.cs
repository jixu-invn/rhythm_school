[System.Serializable]
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
<<<<<<< HEAD
        return GetCurrent().GetNormalizedtimer() - (4 * superMargin);
=======
        List<BeatData> beatDatas = new List<BeatData>();
        int i = current;
        BeatData beat = Beats[i++];
        
        while (i < Beats.Length && beat.GetNormalizedTimer() - currentTime < multiplier * clueDuration)
        {
            if (beat.GetClued() == false)
            {
                beatDatas.Add(beat);
            }

            beat = Beats[i++];
        }

        return beatDatas;
>>>>>>> ClueManaging
    }

    public float ActionTime()
    {
        return GetCurrent().GetNormalizedtimer() - superMargin;
    }

    public float OkTime()
    {
        return GetCurrent().GetNormalizedtimer() - margin;
    }

    public float FailTime()
    {
        return GetCurrent().GetNormalizedtimer() + margin;
    }
}
