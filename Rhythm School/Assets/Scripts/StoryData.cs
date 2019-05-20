[System.Serializable]
public class StoryData
{
    public StoryPhrase[] Dialog;
    private int current = 0;

    public int GetIndex()
    {
        return current;
    }

    public StoryPhrase GetCurrent()
    {
        return Dialog[current];
    }

    public void Next()
    {
        current++;
    }

    public StoryPhrase End()
    {
        return Dialog[Dialog.Length - 1];
    }
}
