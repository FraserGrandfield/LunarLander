using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IEntity
{
    private InputReader _inputReader;
    private CommandProcessor _commandProcessor;
    [SerializeField] private float force = 100f;
    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _commandProcessor = GetComponent<CommandProcessor>();
    }

    private void Update()
    {
        int acceleration = _inputReader.ReadAccelerateInput();
        if (acceleration != 0)
        {
            Debug.Log("accelerate in ship");
            AccelerateCommand accelerateCommand = new AccelerateCommand(this, force);
            _commandProcessor.ExecuteCommand(accelerateCommand);
        }

        int rotation = _inputReader.ReadRotateInput();
        if (rotation != 0)
        {
            RotateCommand rotateCommand = new RotateCommand(this, rotation);
            _commandProcessor.ExecuteCommand(rotateCommand);
        }

        bool undo = _inputReader.ReadUndo();
        if (undo)
        {
            _commandProcessor.undo();
        }
        else
        {
            GravityCommand gravityCommand = new GravityCommand(this, -2.5f);
            _commandProcessor.ExecuteCommand(gravityCommand);
        }
    }
}
