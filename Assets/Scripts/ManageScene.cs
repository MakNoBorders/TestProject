using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
  
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }

}
