using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GoalObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clearText;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Å¬¸®¾î!");

            if (clearText != null)
            {
                clearText.text = "Clear!";
                clearText.gameObject.SetActive(true);
            }
            StartCoroutine(ReturnToMainSceneAfterDelay());
        }

    }
    IEnumerator ReturnToMainSceneAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MainScene");
    }
}