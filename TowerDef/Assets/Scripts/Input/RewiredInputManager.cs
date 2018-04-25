using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
namespace UserInput
{
    public class RewiredInputManager : InputManager
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
        private string _mainHorizontalAxis = "MainHorizontal";
        [SerializeField]
        private string _mainVerticalAxis = "MainVertical";
        [SerializeField]
        private string _subHorizontalAxis = "SubHorizontal";
        [SerializeField]
        private string _subVerticalAxis = "SubVertical";
        [SerializeField]
        private string _menuWheelAxis = "MenuWheel";
        [SerializeField]
        private string _pauseAxis = "Pause";
        [SerializeField]
        private string _interactAxis = "Interact";

        [SerializeField]
        private string _submitAxis = "Submit";
        [SerializeField]
        private string _cancelAxis = "Cancel";
        private Dictionary<int, string> _actions;
        private RewiredInputManager manager;
        private Rewired.Player playerControl;

        protected override void Awake()
        {
            base.Awake();
            SetInstance(this);

            _actions = new Dictionary<int, string>();

            string prefix = !string.IsNullOrEmpty(_playerAxisPrefix) ? _playerAxisPrefix : string.Empty;

            AddAction(InputAction.Jump, prefix + _jumpAxis, _actions);
            AddAction(InputAction.Attack, prefix + _attackAxis, _actions);

            AddAction(InputAction.MainHorizontal, prefix + _mainHorizontalAxis, _actions);
            AddAction(InputAction.MainVertical, prefix + _mainVerticalAxis, _actions);

            AddAction(InputAction.SubHorizontal, prefix + _subHorizontalAxis, _actions);
            AddAction(InputAction.SubVertical, prefix + _subVerticalAxis, _actions);

            AddAction(InputAction.WheelMenu, prefix + _menuWheelAxis, _actions);
            AddAction(InputAction.Pause, prefix + _pauseAxis, _actions);

            AddAction(InputAction.Interact, prefix + _interactAxis, _actions);
            AddAction(InputAction.Submit, prefix + _submitAxis, _actions);

            AddAction(InputAction.Cancel, prefix + _cancelAxis, _actions);

        }

        void Start()
        {
            playerControl = ReInput.players.GetPlayer(0);
        }
        void Update()
        {
            Controller controller = playerControl.controllers.GetLastActiveController();
            if (controller != null)
            {
                switch (controller.type)
                {
                    case ControllerType.Keyboard:
                        isUsingKeyboardAndMouse = true;
                        isUsingController = false;
                        break;
                    case ControllerType.Joystick:
                        isUsingKeyboardAndMouse = false;
                        isUsingController = true;
                        break;
                    case ControllerType.Mouse:
                        isUsingKeyboardAndMouse = true;
                        isUsingController = false;
                        break;
                    case ControllerType.Custom:
                        isUsingKeyboardAndMouse = false;
                        isUsingController = true;
                        break;
                }
            }
        }

        private static void AddAction(InputAction action, string actionName, Dictionary<int, string> actions)
        {
            if (string.IsNullOrEmpty(actionName)) return;
            actions.Add((int)action, actionName);
        }

        public override bool GetButton(InputAction action)
        {
            string actionName = (_actions[(int)action]);
            bool value = Rewired.ReInput.players.GetPlayer(0).GetButton(actionName);
            return value;
        }

        public override bool GetButtonDown(InputAction action)
        {
            string actionName = (_actions[(int)action]);
            bool value = Rewired.ReInput.players.GetPlayer(0).GetButtonDown(actionName);
            return value;
        }

        public override bool GetButtonUp(InputAction action)
        {
            string actionName = (_actions[(int)action]);
            bool value = Rewired.ReInput.players.GetPlayer(0).GetButtonUp(actionName);
            return value;
        }

        public override float GetAxisRaw( InputAction action)
        {
            string actionName = (_actions[(int)action]);
            float value = Rewired.ReInput.players.GetPlayer(0).GetAxisRaw(actionName);
            return value;
        }

        public override float GetAxis( InputAction action)
        {
            string actionName = (_actions[(int)action]);
            float value = Rewired.ReInput.players.GetPlayer(0).GetAxis(actionName);
            return value;
        }
    }
}
