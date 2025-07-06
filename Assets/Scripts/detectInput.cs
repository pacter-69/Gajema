using UnityEngine;
using UnityEngine.InputSystem;

public class detectInput : MonoBehaviour
{
    public enum inputType
    {
        keyboard,
        controller
    }
    public inputType currentType;
    public GameObject UIKeyboard;
    public GameObject UIController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentType = inputType.keyboard;
    }

    // Update is called once per frame
    void Update()
    {
        currentType = ChangeType();
        if (currentType == inputType.keyboard)
        {
            UIController.SetActive(false);
            UIKeyboard.SetActive(true);
        }
        else if (currentType == inputType.controller)
        {
            UIController.SetActive(true);
            UIKeyboard.SetActive(false);
        }
    }

    inputType ChangeType()
    {

        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            return inputType.keyboard;
        }
        else if (Gamepad.current != null && DetectGamepad())
        {
            return inputType.controller;
        }
        return currentType;
    }
    private bool DetectGamepad()
    {
        var gamepad = Gamepad.current;
        return
            gamepad.buttonSouth.wasPressedThisFrame ||
            gamepad.buttonNorth.wasPressedThisFrame ||
            gamepad.buttonEast.wasPressedThisFrame ||
            gamepad.buttonWest.wasPressedThisFrame ||
            gamepad.leftShoulder.wasPressedThisFrame ||
            gamepad.rightShoulder.wasPressedThisFrame ||
            gamepad.leftTrigger.wasPressedThisFrame ||
            gamepad.rightTrigger.wasPressedThisFrame ||
            gamepad.startButton.wasPressedThisFrame ||
            gamepad.selectButton.wasPressedThisFrame ||
            gamepad.leftStickButton.wasPressedThisFrame ||
            gamepad.rightStickButton.wasPressedThisFrame ||
            gamepad.dpad.up.wasPressedThisFrame ||
            gamepad.dpad.down.wasPressedThisFrame ||
            gamepad.dpad.left.wasPressedThisFrame ||
            gamepad.dpad.right.wasPressedThisFrame ||
            gamepad.rightStick.ReadValue().magnitude > 0.2f ||
            gamepad.leftStick.ReadValue().magnitude > 0.2f;
    }

    
}
