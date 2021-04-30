using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : Singleton<SFXController>
{
    protected SFXController() { }

    AudioSource sfxSource;

    public AudioClip player_damage;
    public AudioClip player_dash;
    public AudioClip player_shoot;
    public AudioClip player_death;
    public AudioClip enemy_death;
    public AudioClip enemy_attack;

    // GenericHealth healthbar;
    // int maxHealth;
    // int currentHealth;
    // bool isPlayer = false;
    private Dictionary<string, AudioClip> audio_array;
    void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        sfxSource.ignoreListenerPause = true;
        // if (gameObject.tag == "Player")
        // {
        //     isPlayer = true;
        // }
        // healthbar = GetComponent<GenericHealth>();
        // maxHealth = healthbar.health;
        // currentHealth = maxHealth;
        audio_array = new Dictionary<string, AudioClip>();
        audio_array.Add("player_damage", player_damage);
        audio_array.Add("player_dash", player_dash);
        audio_array.Add("player_shoot", player_shoot);
        audio_array.Add("player_death", player_death);
        audio_array.Add("enemy_death", enemy_death);
        audio_array.Add("enemy_attack", enemy_attack);
    }

    // Update is called once per frame
    void Update()
    {
        // HealthCheck();
        // if (isPlayer)
        // {
        //     if (Input.GetKeyDown(KeyCode.X)) //Attack 
        //     {
        //         PlayAttackSFX();
        //     }
        // }
    }
    // void HealthCheck()
    // {
    //     maxHealth = healthbar.health; //update health bar

    //     if (maxHealth != currentHealth)
    //     {
    //         PlayDamageSFX();//Play Sound
    //         currentHealth = maxHealth;
    //     }
    // }
    // public void PlayDamageSFX()
    // {
    //     sfxSource.clip = takeDamage;
    //     sfxSource.Play();
    // }
    // public void PlayAttackSFX()
    // {
    //     sfxSource.clip = attack;
    //     sfxSource.Play();
    // }
    // public void PlayDeathSFX()
    // {
    //     sfxSource.clip = death;
    //     AudioSource.PlayClipAtPoint(death, transform.position);
    //     Debug.Log("Die Sound");
    // }
    public void play(string audio_name) {
        sfxSource.clip = audio_array[audio_name];
        sfxSource.PlayOneShot(audio_array[audio_name], 1f);
    }
}