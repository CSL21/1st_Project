using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ClickStart()
    {
        SceneManager.LoadScene("FieldScene");
    }

    public void ClickGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ClickReturn()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ClickExit()
    {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

            #else
            Application.Quit();

            #endif
    }
}