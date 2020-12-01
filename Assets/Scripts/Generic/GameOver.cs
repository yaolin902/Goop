using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Start() {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        // whether the player is dead
        if (GameObject.FindWithTag("Player") != null) {
            return;
        }

        GetComponent<Animator>().SetTrigger("pop_up");
        Time.timeScale = 0;
    }

    public void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
