using System;
using System.Collections.Generic;
using UnityEngine;
namespace UserInput {
/*
public class UnityInputManager : InputManager
{
    [SerializeField]
    private string _playerAxisPrefix = "";
    [SerializeField]
    private int _maxNumberOfPlayers = 1;
    [SerializeField]
    private string _jumpAxis = "Jump";
    [SerializeField]
    private string _attackAxis = "Attack";
    [SerializeField]
    private string _moveHorizontalAxis = "Horizontal";
    [SerializeField]
    private string _moveVerticalAxis = "Vertical";
    [SerializeField]
    private string _cameraHorizontalAxis = "CameraHorizontal";
    [SerializeField]
    private string _cameraVerticalAxis = "CameraVertical";
    [SerializeField]
    private string _mouseXAxis = "Mouse X";
    [SerializeField]
    private string _mouseYAxis = "Mouse Y";
    [SerializeField]
    private string _menuWheelAxis = "MenuWheel";
    [SerializeField]
    private string _pauseAxis = "Pause";
    float timeLeft = 0f;
    float reCheckTime = 1.5f;
    private Dictionary<int, string>[] _actions;

    protected override void Awake()
    {
        base.Awake();
        if(InputManager.Instance != null)
        {
            isEnabled = false;
            return;
        }

        SetInstance(this);

        _actions = new Dictionary<int, string>[_maxNumberOfPlayers];

        for(int i = 0; i < _maxNumberOfPlayers; i++)
        {
            Dictionary<int, string> playerActions = new Dictionary<int, string>();
            _actions[i] = playerActions;
            string prefix = !string.IsNullOrEmpty(_playerAxisPrefix) ? _playerAxisPrefix + i : string.Empty;

            AddAction(InputAction.Jump , prefix + _jumpAxis , playerActions);
            AddAction(InputAction.Attack, prefix + _attackAxis, playerActions);

            AddAction(InputAction.MoveHorizontal, prefix + _moveHorizontalAxis , playerActions);
            AddAction(InputAction.MoveVertical, prefix + _moveVerticalAxis , playerActions);

            AddAction(InputAction.CameraHorizontal, prefix + _cameraHorizontalAxis, playerActions);
            AddAction(InputAction.CameraVertical, prefix + _cameraVerticalAxis, playerActions);

            AddAction(InputAction.MouseX, prefix + _mouseXAxis, playerActions);
            AddAction(InputAction.MouseY, prefix + _mouseYAxis, playerActions);

            AddAction(InputAction.WheelMenu, prefix + _menuWheelAxis, playerActions);
            AddAction(InputAction.Pause, prefix + _pauseAxis, playerActions);
        }
    }

    void Update()
    {

        float mouseX = Input.GetAxis ("Mouse X");
        float mouseY = Input.GetAxis ("Mouse Y");
        float cursorMagnitude = Mathf.Abs(mouseX) + Mathf.Abs(mouseY);
        if(timeLeft < 0 || cursorMagnitude > 0) 
        {
            // i am moving the mouse
            timeLeft = reCheckTime;
            // if timeleft is zero we have to check it again
            if(cursorMagnitude > 0) 
                isMouseMoving = true;
            else 
                isMouseMoving = false;
        }
        else 
        
        timeLeft -= Time.deltaTime;
    }

    private static void AddAction(InputAction action , string actionName , Dictionary<int , string> actions)
    {
        if (string.IsNullOrEmpty(actionName)) return;
        actions.Add((int)action, actionName);
    }

    public override bool GetButton(int playerId, InputAction action)
    {
        bool value = Input.GetButton(_actions[playerId][(int)action]);
        return value;
    }

    public override bool GetButtonDown(int playerId, InputAction action)
    {
        bool value = Input.GetButtonDown(_actions[playerId][(int)action]);
        return value;
    }

    public override bool GetButtonUp(int playerId, InputAction action)
    {
        bool value = Input.GetButtonUp(_actions[playerId][(int)action]);
        return value;
    }

    public override float GetAxisRaw(int playerId, InputAction action)
    {
        float value = Input.GetAxisRaw(_actions[playerId][(int)action]);
        return value;
    }

        public override float GetAxis(int playerId, InputAction action)
    {
        float value = Input.GetAxis(_actions[playerId][(int)action]);
        return value;
    }

    public override void OnAddLastActiveControllerChanged()
    {
        throw new NotImplementedException();
    }

    public override void OnRemoveLastActiveController()
    {
        throw new NotImplementedException();
    }

    public override void OnClearLastActiveController()
    {
        throw new NotImplementedException();
    }
}
*/
}
