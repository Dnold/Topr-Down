using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public float noiseSpeed;
    public AudioSource audioSource;
    public AudioClip[] clips;

    bool canGetHit = true;
    bool canAttack = true;
    public float attackSpeed = 2f;
    public float attackTime;
    public float attackRange;
    bool hitPlayer;
    bool playerInRange;
    bool dead = false;
    public Player player;

    public Slider healthSlider;
    public float newTargetTime;
    public Rigidbody2D rg;
    public Animator animator;
    public AnimationClip DeathAnimation;
    public ParticleSystem deathParticles;

    public AudioClip deathSound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        health = maxHealth;
        //StartCoroutine(MakeNoise());
        StartCoroutine(Attack());
        StartCoroutine(searchNewTarget());
    }
    IEnumerator searchNewTarget()
    {
        while (!dead)
        {
            GameObject tempPoint = Instantiate(new GameObject(), RandomPointOnXYCircle(player.transform.position, 2f), Quaternion.identity);
            GetComponent<AIDestinationSetter>().target = tempPoint.transform;
            yield return new WaitForSeconds(newTargetTime);
            Destroy(tempPoint);
        }
    }
    Vector3 RandomPointOnXYCircle(Vector2 center, float radius)
    {
        float angle = Random.Range(0, 2f * Mathf.PI);
        return center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
    }
    IEnumerator MakeNoise()
    {
        while (!dead)
        {
            
            audioSource.clip = SelectRandomAudioClip();
            audioSource.Play();
            yield return new WaitForSeconds(noiseSpeed);
        }
    }
    AudioClip SelectRandomAudioClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
    public void Update()
    {
        if (!dead)
        {
            healthSlider.value = health;

            playerInRange = CheckIfPlayerInRange();
        }
    }
    public void SubHealth(int amount)
    {
        if (canGetHit)
        {
            if (health > amount)
            {
                health -= amount;
            }
            else
            {
                StartCoroutine(Death());
            }
        }
    }
    IEnumerator Death()
    {
        audioSource.clip = deathSound; audioSource.Play();
        deathParticles.Play();
        GetComponent<AIDestinationSetter>().enabled = false;
        dead = true;
        animator.Play(DeathAnimation.name);
        Destroy(gameObject, 0.5f);
        this.enabled = false;
        yield return new WaitForSeconds(2f);


    }
    public bool CheckIfPlayerInRange()
    {
        Vector2 randPoint = Random.insideUnitCircle;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= attackRange)
        {
            return true;
        }
        return false;
    }
    public IEnumerator Attack()
    {
        while (!dead)
        {
            if (playerInRange)
            {

                yield return ColorShift(Color.yellow, attackSpeed, 0.5f);
                if (playerInRange)
                {
                    audioSource.clip = SelectRandomAudioClip();
                    audioSource.Play();
                    yield return Knockback(20, (Vector2)(player.transform.position - transform.position).normalized);
                    if (hitPlayer)
                    {
                        player.SubHealth(10);
                        hitPlayer = false;

                    }
                }

            }
            yield return new WaitForEndOfFrame();
        }

    }
    public void GetHit(Vector2 dir, int distance)
    {
        if (canGetHit)
        {
            SubHealth(distance);
            StartCoroutine(Knockback(distance, dir));
            StartCoroutine(ProtectionTime(0.5f));

        }
    }
    IEnumerator Knockback(int amount, Vector2 dir)
    {
        if (!dead) { 
        for (float i = 0; i < amount; i += Time.deltaTime * 100
                )
        {
            transform.Translate(((dir * i) * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
        }
    }
    IEnumerator ProtectionTime(float protectionTime)
    {
        canGetHit = false;
        yield return ColorShift(Color.red, protectionTime, 1f);
        canGetHit = true;
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
    IEnumerator SquishAndDesquish(float durationInSec, float squishFactor, float squishSpeed)
    {
        for (float i = 0; i < durationInSec; i += squishSpeed * 2)
        {
            transform.localScale = new Vector2(0, transform.localScale.y - squishFactor);
            yield return new WaitForSeconds(squishSpeed);
            transform.localScale = new Vector2(0, transform.localScale.y + squishFactor);
            yield return new WaitForSeconds(squishSpeed);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            hitPlayer = true;
        }
    }
}
