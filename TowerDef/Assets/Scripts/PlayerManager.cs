
using System;
using UnityEngine;
using UserInput;
public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Instance { get; private set; }
    public Player player;
	private IInputManager input;
	public LayerMask buildableLayers;
	public void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}else 
		{
			Destroy(gameObject);
		}
	}

	public void Update()
	{
	}



    /* Returns a world point of were the cursor is in worldspace , working with controller aswell */
}
