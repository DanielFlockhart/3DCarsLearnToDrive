using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour
{
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 6){
            SceneManager.LoadScene("Menu");
        }
    }
}
