using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public Animator game_over_animator;
    public Animator game_pause_animator;
    void Start() {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        // whether the player is dead
        if (GameObject.FindWithTag("Player").GetComponent<GenericHealth>().health == 0) {
            game_over();
        }

        if (Input.GetKey(KeyCode.Escape)) {
            game_pause();
        }

    }

    void game_over() {
        SFXController.Instance.play("player_death");
        game_over_animator.SetTrigger("pop_up");
        Time.timeScale = 0;
    }

    void game_pause() {
        game_pause_animator.SetBool("pause", true);
        Time.timeScale = 0;
    }

    public void game_unpause() {
        StartCoroutine(wait());
        game_pause_animator.SetBool("pause", false);
    }

    IEnumerator wait() {
        yield return new WaitForSecondsRealtime(0.9f);
        Time.timeScale = 1;
    }

}
