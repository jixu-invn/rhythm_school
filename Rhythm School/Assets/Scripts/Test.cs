using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            if (e.isKey && e.type == EventType.KeyDown && e.keyCode != KeyCode.None)
            {
                Debug.Log("Down = " + e.keyCode + " || time = " + Time.timeSinceLevelLoad);
            }
        }
        else
        {
            Event e = Event.current;
            if (e.isKey && e.type == EventType.KeyUp && e.keyCode != KeyCode.None)
            {
                Debug.Log("Up = " + e.keyCode + " || time = " + Time.timeSinceLevelLoad);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.Play("Potion1");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("Potion1_test");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetTrigger("LastAnimation");
        }
    }
}
