using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour
{
    // Splash screen that is shown before program starts
    float timer;

    void Update()
    {
        // After 6 seconds the menu scene is loaded
        timer += Time.deltaTime;
        if (timer > 6){
            SceneManager.LoadScene("Menu");
        }
    }
}
