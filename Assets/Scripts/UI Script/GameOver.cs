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
        if (GameObject.FindWithTag("Player").GetComponent<GenericHealth>().health != 0) {
            return;
        }

        GetComponent<Animator>().SetTrigger("pop_up");
        Time.timeScale = 0;
    }

}
