using UnityEngine
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class EmotionManager : MonoBehaviour
{
   private Emotion cleo;
   private Emotion chord;
   private Emotion audra;
   private Emotion aladdin;

   public void playAnimation(StoryPhrase storyPhrase) {
        for(int i = 0 ; i < storyPhrase.EmotionTab.Length ; i++)
        {
            getCharacter(storyPhrase.CurrentCharacter.ToString()).play(storyPhrase.EmotionTab[i].ToString());
        }
   }

   private Emotion getCharacter(string c)
   {
   		switch(c) {
   			case "Cléo":
   				return cleo;
   			case "Chord":
   				return chord;
   			case "Audra":
   				return audra;
   			case "Aladdin":
   				return aladdin;
   			default:
   				return null;
   		}
   }

}
