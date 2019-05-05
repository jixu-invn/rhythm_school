[System.Serializable]
public class MusicData
{
    public enum Check { Idle, Fail, Ok };

    public float margin = 0.05f;
    public float superMargin = 0.5f;  

    public BeatData[] Beats;
    public Mapper[] Mappers;
    private int current = 0;
    
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

    public float ActionTime()
    {
        return GetCurrent().Timer - superMargin;
    }

    public float OkTime()
    {
        return GetCurrent().Timer - margin;
    }

    public float FailTime()
    {
        return GetCurrent().Timer + margin;
    }
}
