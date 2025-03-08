using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartEasyGameBtn()
    {
        // TODO: Easy Game Configuration
        SceneManager.LoadScene("MainScene");
    }

    public void StartHardGameBtn()
    {
        // TODO: Hard Game Configuration
        SceneManager.LoadScene("MainScene");
    }
}
