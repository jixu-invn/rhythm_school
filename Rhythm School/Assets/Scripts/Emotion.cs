using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class Emotion : MonoBehaviour
{
   private AnimationController animationController;

    private void Awake() {
        animationController = GetComponent<AnimationCotnroller>();
    }   

    private void Start(){
              
    }

    private void Update()
    {
       
    }

    public void play(string emotion) {
        animationController.play(emotion);
    }


}
