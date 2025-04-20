using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadSampleScene()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
