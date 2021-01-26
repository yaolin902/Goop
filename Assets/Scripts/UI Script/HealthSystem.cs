using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int health;
    public int num_of_hearts;

    public Image[] hearts;
    public Sprite full_heart;
    public Sprite empty_heart;

    void Update() {
        health = GameObject.FindWithTag("Player").GetComponent<GenericHealth>().health;

        for (int i = 0; i < hearts.Length; i++) {
            if (i < health)
                hearts[i].sprite = full_heart;
            else
                hearts[i].sprite = empty_heart;

            if (i < num_of_hearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
}
