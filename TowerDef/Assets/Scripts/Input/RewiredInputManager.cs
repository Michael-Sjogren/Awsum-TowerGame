using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
namespace UserInput {
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

        float timeLeft = 0f;
        float reCheckTime = 1.5f;
        private Dictionary<int, string>[] _actions;
        private RewiredInputManager manager;
        private Rewired.Player playerControl;

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

                AddAction(InputAction.MainHorizontal, prefix + _mainHorizontalAxis , playerActions);
                AddAction(InputAction.MainVertical, prefix + _mainVerticalAxis , playerActions);

                AddAction(InputAction.SubHorizontal, prefix + _subHorizontalAxis, playerActions);
                AddAction(InputAction.SubVertical, prefix + _subVerticalAxis, playerActions);

                AddAction(InputAction.WheelMenu, prefix + _menuWheelAxis, playerActions);
                AddAction(InputAction.Pause, prefix + _pauseAxis, playerActions);

                AddAction(InputAction.Interact, prefix + _interactAxis , playerActions );       
                AddAction(InputAction.Submit, prefix + _submitAxis , playerActions );
                                
                AddAction(InputAction.Cancel, prefix + _cancelAxis , playerActions );                
            }
        }

        void Start()
        {
            playerControl = ReInput.players.GetPlayer(0);
        }
        void Update()
        {
            Controller controller = playerControl.controllers.GetLastActiveController();
            if(controller != null) {
                switch(controller.type) {
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

        private static void AddAction(InputAction action , string actionName , Dictionary<int , string> actions)
        {
            if (string.IsNullOrEmpty(actionName)) return;
            actions.Add((int)action, actionName);
        }

        public override bool GetButton(int playerId, InputAction action)
        {
            string actionName = (_actions[playerId][(int)action]);
            bool value =  Rewired.ReInput.players.GetPlayer(playerId).GetButton(actionName) ;
            return value;
        }

        public override bool GetButtonDown(int playerId, InputAction action)
        {
            string actionName = (_actions[playerId][(int)action]);
            bool value =  Rewired.ReInput.players.GetPlayer(playerId).GetButtonDown(actionName) ;
            return value;
        }

        public override bool GetButtonUp(int playerId, InputAction action)
        {
            string actionName = (_actions[playerId][(int)action]);
            bool value =  Rewired.ReInput.players.GetPlayer(playerId).GetButtonUp(actionName) ;
            return value;
        }

        public override float GetAxisRaw(int playerId, InputAction action)
        {
            string actionName = (_actions[playerId][(int)action]);
            float value =  Rewired.ReInput.players.GetPlayer(playerId).GetAxisRaw(actionName) ;
            return value;
        }

        public override float GetAxis(int playerId, InputAction action)
        {
            string actionName = (_actions[playerId][(int)action]);
            float value =  Rewired.ReInput.players.GetPlayer(playerId).GetAxis(actionName) ;
            return value;
        }
    }
}
