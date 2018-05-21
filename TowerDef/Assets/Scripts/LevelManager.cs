
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> 
{
	[HideInInspector]
	public float percentage = 0;
	public void ChangeLevel(string name)
	{
		percentage = 0;
		AsyncOperation operation = SceneManager.LoadSceneAsync(name);
		percentage = operation.progress;
	}
}
