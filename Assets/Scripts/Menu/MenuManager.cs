using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void LoadMenuScreen()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("MenuScreen");
    }

}
