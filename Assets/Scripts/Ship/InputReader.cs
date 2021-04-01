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
        RotateAntiClockWise,
        Resume,
        Escape,
        LeftArrowKey,
        RightArrowKey
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
    
    public InputKey? ReadContinueInput()
    {
        if (Input.GetKey(KeyCode.E))
        {
            return InputKey.Resume;
        } 
        return null;
    }
    
    public InputKey? ReadPauseInput()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            return InputKey.Escape;
        } 
        return null;
    }
    
    public InputKey? ReadLeftArrowKey()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return InputKey.Resume;
        } 
        return null;
    }
    
    public InputKey? ReadRightArrowKey()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            return InputKey.Resume;
        } 
        return null;
    }
}
