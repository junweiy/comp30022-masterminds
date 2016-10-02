using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	

    public void LoadPlay()
    {
        SceneManager.LoadScene("newversion/scenes/Ready");

    }

    public void LoadReplay()
    {
        SceneManager.LoadScene("newversion/scenes/Replay");
    }


}
