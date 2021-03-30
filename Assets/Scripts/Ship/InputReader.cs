using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public int ReadAccelerateInput()
    {
        int keydown = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            keydown = 1;
        }

        return keydown;
    }
    
    public bool ReadAccelerateInputKeyUp()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }

    public int ReadRotateInput()
    {
        int rotation = 0;
        if (Input.GetKey(KeyCode.A))
        {
            rotation = 1;
        } else if (Input.GetKey(KeyCode.D))
        { 
            rotation = -1;
        }
        return rotation;
    }

    public bool ReadPauseGameInput()
    {
        return Input.GetKeyUp(KeyCode.Escape);
    }
}