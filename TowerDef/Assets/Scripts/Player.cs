
using UnityEngine;
using TMPro;
using UserInput;
using System;
using System.Collections;

public class Player : MonoBehaviour , IDamagable
{
    public int playerMoney = 0;
    private Vector3 playerCursor;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;
    private IInputManager input;
    public bool IsAlive{get;set;}
    public int health;
    public float buildRange = 2f;

    public float Health {get{return health;} set {health = (int)value;}}
    public float MaxHealth { get; set; }

    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private int coinDropAmount = 3;
    private Coroutine coinDropRoutine;
    [SerializeField]
    private AudioEvent playerHurtAudio;

    void Start()
    {
        input = InputManager.Instance;
        if(moneyText != null)
            moneyText.SetText(((int)playerMoney).ToString());
        if(healthText != null)
            healthText.SetText(((int)Health).ToString());
    }


    // Update is called once per frame
    public void Update()
    {
        UpdateCursor();
    }
    private void UpdateCursor()
    {

        float cellSize = 1;
        float offset = (cellSize / 2f);
    
        Vector3 direction;
        if (input.isUsingController)
        {
            float xAxis = input.GetAxisRaw( InputAction.SubHorizontal);
            float zAxis = input.GetAxisRaw( InputAction.SubVertical);
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
                return input.GetAxis(InputAction.SubHorizontal);
            }
        }
        else
        {
            return input.GetAxis(InputAction.SubHorizontal);
        }
        return 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(coinDropRoutine == null)
            {
                if(playerMoney >= 3)
                {
                    coinDropRoutine = StartCoroutine(DropCoins());
                }
            }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (coinDropRoutine == null)
            {
                if (playerMoney >= 3)
                {
                    coinDropRoutine = StartCoroutine(DropCoins());
                }
            }
        }
    }

    public IEnumerator DropCoins()
    {
        Coin[] coins = new Coin[coinDropAmount];
        playerHurtAudio.Play(this.GetComponent<AudioSource>());
        for (int i = 0; i < coinDropAmount; i++)
        {
            var obj = Instantiate(coinPrefab , transform.position ,Quaternion.identity);
            var coin = obj.GetComponent<Coin>();
            coin.enabled = false;
            coin.SetRadius(0f);
            coins[i] = coin;
            BuyItem(1);
        }

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < coins.Length; i++)
        {
            var coin = coins[i];
            coin.SetRadius(coin.pickupRadius);
            coin.enabled = true;
        }
        coinDropRoutine = null;
    }

    public Vector2 GetMenuSelectionAxis()
    {
        float y = 0f;
        float x = 0f;

        if (input.isUsingController)
        {
            y = input.GetAxisRaw( InputAction.SubVertical);
            x = input.GetAxisRaw( InputAction.SubHorizontal);
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
        
        if ( ((int) Health - amount) <= 0) 
        {
         GameManager.instance.gameOver = true;
        }
        Health -= amount;
        healthText.SetText(( (int)Health).ToString());
            
    }

    public IEnumerator Die(float delay)
    {
        throw new NotImplementedException();
    }

    public bool CanAfford(int price)
    {
        return playerMoney >= price;
    }

    public float GetHealth()
    {
        return Health;
    }
}
