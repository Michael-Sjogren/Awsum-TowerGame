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
        private string _abillityAxis1 = "Abillity1";
        [SerializeField]
        private string _abillityAxis2 = "Abillity2";
        [SerializeField]
        private string _abillityAxis3 = "Abillity3";
        [SerializeField]
        private string _abillityAxis4 = "Abillity4";
        [SerializeField]
        private string _abillityAxis5 = "Abillity5";

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
            AddAction(InputAction.Jump, _playerAxisPrefix + _jumpAxis, _actions);

            AddAction(InputAction.Abillity1, _playerAxisPrefix + _abillityAxis1, _actions);
            AddAction(InputAction.Abillity2, _playerAxisPrefix + _abillityAxis2, _actions);
            AddAction(InputAction.Abillity3, _playerAxisPrefix + _abillityAxis3, _actions);
            AddAction(InputAction.Abillity4, _playerAxisPrefix + _abillityAxis4, _actions);
            AddAction(InputAction.Abillity5, _playerAxisPrefix + _abillityAxis5, _actions);


            AddAction(InputAction.MainHorizontal, _playerAxisPrefix + _mainHorizontalAxis, _actions);
            AddAction(InputAction.MainVertical, _playerAxisPrefix + _mainVerticalAxis, _actions);

            AddAction(InputAction.SubHorizontal, _playerAxisPrefix + _subHorizontalAxis, _actions);
            AddAction(InputAction.SubVertical, _playerAxisPrefix + _subVerticalAxis, _actions);

            AddAction(InputAction.WheelMenu, _playerAxisPrefix + _menuWheelAxis, _actions);
            AddAction(InputAction.Pause, _playerAxisPrefix + _pauseAxis, _actions);

            AddAction(InputAction.Interact, _playerAxisPrefix + _interactAxis, _actions);
            AddAction(InputAction.Submit, _playerAxisPrefix + _submitAxis, _actions);

            AddAction(InputAction.Cancel, _playerAxisPrefix + _cancelAxis, _actions);

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
