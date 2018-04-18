using Rewired;

namespace UserInput {
    public interface IInputManager
    {
        bool isUsingKeyboardAndMouse {get; set;}
        bool isUsingController {get; set;}
        bool isEnabled { get; set; }
        bool isMouseMoving {get ; set;}
        bool GetButton(int playerId, InputAction action);
        bool GetButtonDown(int playerId, InputAction action);
        bool GetButtonUp(int playerId, InputAction action);
        float GetAxis(int playerId, InputAction action);
        float GetAxisRaw(int playerId, InputAction action);
    }

}

