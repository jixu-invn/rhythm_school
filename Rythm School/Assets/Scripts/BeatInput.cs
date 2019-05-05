using System.Collections;
using UnityEngine;

[System.Serializable]
public class BeatInput
{
    public enum ActionType { Down, Up };

    public int Action;
    public ActionType actionType;
    private bool fDone = false;

    public bool GetfDone()
    {
        return fDone;
    }

    public void Done()
    {
        fDone = true;
    }
}
