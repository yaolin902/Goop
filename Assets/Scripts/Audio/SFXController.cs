using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    AudioSource sfxSource;

    public AudioClip takeDamage;
    public AudioClip attack;
    public AudioClip death;

    GenericHealth healthbar;
    int maxHealth;
    int currentHealth;
    bool isPlayer = false;
    void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        if (gameObject.tag == "Player")
        {
            isPlayer = true;
        }
        healthbar = GetComponent<GenericHealth>();
        maxHealth = healthbar.health;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthCheck();
        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.X)) //Attack 
            {
                PlayAttackSFX();
            }
        }
    }
    void HealthCheck()
    {
        maxHealth = healthbar.health; //update health bar

        if (maxHealth != currentHealth)
        {
            PlayDamageSFX();//Play Sound
            currentHealth = maxHealth;
        }
    }
    public void PlayDamageSFX()
    {
        sfxSource.clip = takeDamage;
        sfxSource.Play();
    }
    public void PlayAttackSFX()
    {
        sfxSource.clip = attack;
        sfxSource.Play();
    }
    public void PlayDeathSFX()
    {
        sfxSource.clip = death;
        AudioSource.PlayClipAtPoint(death, transform.position);
        //Debug.Log("Die Sound");
    }
}