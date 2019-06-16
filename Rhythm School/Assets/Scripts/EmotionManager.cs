using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    public static EmotionManager emotionManager;

    public Emotion cleo;
    public Emotion chord;
    public Emotion audra;
    public Emotion aladdin;

    private void Awake()
    {
        if (emotionManager == null)
        {
            emotionManager = this;
        }
        else
        {
            if (emotionManager != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void playAnimation(StoryPhrase storyPhrase)
    {
        for(int i = 0 ; i < storyPhrase.EmotionTab.Length ; i++)
        {
            getCharacter(i).play(storyPhrase.EmotionTab[i].ToString());
        }
    }

    private Emotion getCharacter(int i)
    {
   	    switch(i)
        {
   		    case 0:
   			    return cleo;
   		    case 1:
   			    return chord;
   		    case 2:
   			    return audra;
   		    case 3:
   			    return aladdin;
   		    default:
   			    return null;
   	    }
    }

}
