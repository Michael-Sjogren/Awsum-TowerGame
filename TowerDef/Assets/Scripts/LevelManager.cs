
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager> 
{
    [SerializeField]
    private GameObject loadingPanel;
    [SerializeField]
    private Image progressbar;
    [SerializeField]
    private TextMeshProUGUI loadText;

    public void LoadSceneInstant(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadLevelWithProgressBar(int sceneIndex)
	{
        StartCoroutine(LoadASync(sceneIndex));
	}

    private IEnumerator LoadASync(int sceneIndex)
    {
        loadingPanel.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            progressbar.fillAmount = operation.progress / .9f;
            yield return null;
        }
    }
}
