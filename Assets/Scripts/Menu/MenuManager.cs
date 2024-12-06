using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void LoadMenuScreen()
    {
        SceneManager.LoadScene("CapstoneMain");
        Debug.Log("MenuScreen");
    }

}
