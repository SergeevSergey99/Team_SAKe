
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void newGame()
    {
        SceneManager.LoadScene("Game");
    }
}