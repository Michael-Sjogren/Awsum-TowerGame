
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="New Level List" , menuName = "New list of levels")]

public class LevelData : ScriptableObject
{
    public Level[] listOfLevels;
}

[Serializable]
public struct Level
{
    public string name;
    public string sceneName;
    public int health;
    public int startingMoney;
}


