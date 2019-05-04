using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int score;
    private float groove;
    private MusicData musicData;
    private MusicData.BeatData beatData;
    private float timer;

    private void Start()
    {
        MusicData.superMargin = 0.243f; // 1/2 beat duration
    }

}
