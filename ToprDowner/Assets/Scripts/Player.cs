using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Health
    public int maxHealth;
    public int health;

    //UI
    public Slider healthSlider;
    public UIDamage uiDamage;

    //Sound
    public AudioSource walkAudioSource;
    public AudioSource attackAudioSource;
    public AudioClip[] clips;
    void Start()
    {
        health = maxHealth; 
    }

    public void SubHealth(int amount)
    {
        if (health > amount)
        {
            health -= amount;
            StartCoroutine(ColorShift(Color.red, 1f, 0.2f));
            uiDamage.DisplayDamage(amount);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        healthSlider.value = health;
    }
    public IEnumerator Knockback(int amount, Vector2 dir)
    {
        for (int i = 0; i < amount; i++)
        {
            transform.Translate(((dir * i) * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator ColorShift(Color color, float durationInSec, float flickerSpeedInSec)
    {
        for (float i = 0; i < durationInSec; i += flickerSpeedInSec)
        {

            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(flickerSpeedInSec);
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
