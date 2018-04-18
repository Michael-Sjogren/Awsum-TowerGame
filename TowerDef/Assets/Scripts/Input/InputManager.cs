using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
namespace UserInput {
    public abstract class InputManager : MonoBehaviour, IInputManager
    {
        #region oldcode
        /*
        // Listens for the right joystick axies on a controller and mouse xy for pc 
        // horizontal(x) and vertical(z)
        public static Vector3 GetSecondaryAxis()
        {
            float x = 0.0f;
            float y = 0.0f;
            // joystick 
            x += Input.GetAxis("J_RightHorizontal");
            y += Input.GetAxis("J_RightVertical");
            // keyboard 
            Vector2 mousePos = Input.mousePosition;
            mousePos.x -= Screen.width  / 2;
            mousePos.y -= Screen.height / 2;
            mousePos.Normalize();
            x += mousePos.x;
            y += mousePos.y;

            return new Vector3(Mathf.Clamp(x , -1f  , 1f ) , 0f , Mathf.Clamp(y , -1f , 1f ));	
        }

        */
        #endregion

        private static InputManager _instance;
        public static IInputManager Instance { get { return _instance; } }
        public static void SetInstance(InputManager instance)
        {
            if(InputManager._instance == instance)
            {
                return;
            }

            if(InputManager._instance != null)
            {
                InputManager._instance.enabled = false;
            }

            InputManager._instance = instance;
        }
        private bool _dontDestroyOnLoad = true;

        public virtual bool isEnabled
        {
            get
            {
                return this.isActiveAndEnabled;
            }
            set
            {
                this.enabled = value;
            }
        }

        public bool isMouseMoving
        {
            get;
            set;
        }

        public bool isUsingKeyboardAndMouse
        {
            get;
            set;
        }

        public bool isUsingController { get; set; }

        protected virtual void Awake()
        {
            if(_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(this.transform.root.gameObject);
            }
        }

        public abstract float GetAxis(int playerId, InputAction action);

        public abstract float GetAxisRaw(int playerId, InputAction action);
        public abstract bool GetButton(int playerId, InputAction action);

        public abstract bool GetButtonDown(int playerId, InputAction action);

        public abstract bool GetButtonUp(int playerId, InputAction action);
    }
}