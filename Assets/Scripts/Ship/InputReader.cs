using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public enum InputKey
    {
        SpaceDown,
        SpaceUp,
        RotateClockWise,
        RotateAntiClockWise
    };
    
    public InputKey? ReadAccelerateInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return InputKey.SpaceDown;
        }
        return null;
    }
    
    public InputKey? ReadAccelerateInputKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            return InputKey.SpaceUp;
        }
        return null;
    }

    public InputKey? ReadRotateInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            return InputKey.RotateAntiClockWise;
        } else if (Input.GetKey(KeyCode.D))
        {
            return InputKey.RotateClockWise;
        }
        return null;
    }
}
