
using UnityEngine;
using TMPro;
using UserInput;
using System;

public class Player : MonoBehaviour , IDamagable
{

    public int playerHealth = 0;
    public int playerMoney = 0;
    private Vector3 playerCursor;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;
    private IInputManager input;
    public float buildRange = 4f;

    public Attributes attributes;
    public Attributes Attributes{ get; set;}

    public bool IsAlive{get;set;}

    void Start()
    {
        input = InputManager.Instance;
        if(moneyText != null)
            moneyText.SetText(playerMoney.ToString());
        if(healthText != null)
            healthText.SetText(playerHealth.ToString());
    }


    // Update is called once per frame
    public void Update()
    {
        UpdateCursor();
    }
    private void UpdateCursor()
    {

        float cellSize = 1;
        float y = 0f;
        float offset = (cellSize / 2f);
    
        Vector3 direction;
        if (input.isUsingController)
        {
            float xAxis = input.GetAxisRaw(0, InputAction.SubHorizontal);
            float zAxis = input.GetAxisRaw(0, InputAction.SubVertical);
            playerCursor = playerCursor + Vector3.forward / cellSize;
            direction = new Vector3( this.transform.right.x * xAxis , 0f , this.transform.forward.z * zAxis).normalized / cellSize;
            playerCursor += direction;

        }

        else if (input.isUsingKeyboardAndMouse)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200f, PlayerManager.Instance.buildableLayers))
            {

                float x = ((int)hit.point.x / cellSize) + (cellSize / 2f);
                float z = ((int)hit.point.z / cellSize) + (cellSize / 2f);
                playerCursor = new Vector3(x, hit.point.y ,  z);
            }
        }
    }


    public void ReciveMoney(int amount)
    {
        playerMoney += amount;
        moneyText.SetText(playerMoney.ToString());
    }
    public Vector3 GetPlayerCursor()
    {
        return playerCursor;
    }

    public void BuyItem(int cost)
    {
        playerMoney -= cost;
        moneyText.SetText(playerMoney.ToString());
    }

    public float GetCameraControls()
    {
        if (input.isUsingKeyboardAndMouse)
        {
            if (Input.GetMouseButtonDown(1))
            {
                return input.GetAxis(0, InputAction.SubHorizontal);
            }
        }
        else
        {
            return input.GetAxis(0, InputAction.SubHorizontal);
        }
        return 0;
    }

    public Vector2 GetMenuSelectionAxis()
    {
        float y = 0f;
        float x = 0f;

        if (input.isUsingController)
        {
            y = input.GetAxisRaw(0, InputAction.SubVertical);
            x = input.GetAxisRaw(0, InputAction.SubHorizontal);
        }
        if (input.isUsingKeyboardAndMouse)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);

            // clamp mouse to area
            x = mousePos.x - center.x;
            y = mousePos.y - center.y;
        }

        Vector2 axis = new Vector2(x, y);
        return axis;
    }

    public void TakeDamage(float amount)
    {
        playerHealth--;
        if (playerHealth <= 0) GameManager.instance.gameOver = true;
        healthText.SetText(playerHealth.ToString());
    }
}
