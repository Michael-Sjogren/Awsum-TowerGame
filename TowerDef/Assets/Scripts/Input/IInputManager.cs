using Rewired;

namespace UserInput {
    public interface IInputManager
    {
        bool isUsingKeyboardAndMouse {get; set;}
        bool isUsingController {get; set;}
        bool isMouseMoving {get ; set;}
        bool GetButton(InputAction action);
        bool GetButtonDown( InputAction action);
        bool GetButtonUp(InputAction action);
        float GetAxis( InputAction action);
        float GetAxisRaw( InputAction action);
    }

}

