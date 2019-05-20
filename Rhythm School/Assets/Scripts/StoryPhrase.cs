[System.Serializable]
public class StoryPhrase
{
    public enum character {Cleo, Chord, Audra, Aladdin}
    public enum emotion {Neutral, Stressed, Angry, Happy, Euphoric, Pretentious, Evasive, Sad, Desperate, Panicked, Laughing, Shouting};
    public string Phrase;
    public character CurrentCharacter;
    public emotion[] EmotionTab;
}
