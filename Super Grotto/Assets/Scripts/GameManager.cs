using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startingTransition;
    [SerializeField] private GameObject endTransition;

    void Start()
    {
        startingTransition.SetActive(true);
        Invoke("DisableStartingTransition", 1.5f);
    }

    void DisableStartingTransition()
    {
        startingTransition.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            endTransition.SetActive(true);
            Invoke("LoadScene", 1.3f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
