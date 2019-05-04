using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicData
{
    static public float margin = 0.05f;
    static public float superMargin = 0.5f;

    public enum Check { Idle, Fail, Ok };
    public enum InputType { Down, Up };

    public class BeatData
    {
        public class BeatInput
        {
            public KeyCode input { get; private set; }
            public InputType inputType { get; private set; }

            public BeatInput(KeyCode _input, InputType _inputType)
            {
                input = _input;
                inputType = _inputType;
            }
        }

        public float Timer { get; private set; }
        public List<BeatInput> Inputs { get; private set; }
        public int NextAnimation { get; private set; }

        public BeatData(float _timer, int _nextAnim)
        {
            Timer = _timer;
            Inputs = new List<BeatInput>();
            NextAnimation = _nextAnim;
        }
    }

    private List<BeatData> beats;
    private int current = 0;

    public MusicData()
    {
        beats = new List<BeatData>();
    }

    public MusicData(int _current)
    {
        beats = new List<BeatData>();
        current = _current;
    }

    public BeatData getCurrent()
    {
        return beats[current];
    }

    public BeatData Next()
    {
        current++;
        return getCurrent();
    }

    public BeatData End()
    {
        return beats[beats.Count - 1];
    }
}
