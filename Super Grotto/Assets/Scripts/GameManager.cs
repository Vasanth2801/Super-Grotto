using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startingTransition;
    [SerializeField] private GameObject endTransition;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneLoad();
        }

    }

    void SceneLoad()
    {
        startingTransition.SetActive(true);
        Invoke("DisableStartingTransition", 1.5f);
        endTransition.SetActive(true);
        Invoke("LoadScene", 1f);
        endTransition.SetActive(false);
    }

    void DisableStartingTransition()
    {
        startingTransition.SetActive(false);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
