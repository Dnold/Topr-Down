using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public AnimationController animController;
    public Player player;
    bool walkSoundplaying;

    float[] currentTime = { 0, 0 };
    float[] speedboosts = { 0, 0 };

    public float speed = 5f;
    public float multiplier;
    public float maxSpeed = 8f;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        float[] axies = { moveX, moveY };
        AccelerateInAxis(axies);
        Move(new Vector3(speed * speedboosts[0] * moveX, speed * speedboosts[1] * moveY));
    }
    IEnumerator playSound()
    {
        if (!walkSoundplaying)
        {
            walkSoundplaying = true;
            yield return new WaitForSeconds(0.3f);
            player.walkAudioSource.clip = player.clips[1];
            player.walkAudioSource.Play();
            walkSoundplaying = false;
        }
    }

    void Move(Vector2 moveDir)
    {
        animController.velocity = Mathf.Abs(moveDir.magnitude);
        transform.Translate(moveDir * Time.deltaTime);
    }
    void AccelerateInAxis(float[] axis)
    {
        for (int i = 0; i < axis.Length; i++)
        {
            if (Mathf.Abs(axis[i]) > 0)
            {
                StartCoroutine(playSound());
                currentTime[i] += Time.deltaTime;
                speedboosts[i] = (currentTime[i]) * multiplier;
            }
            else
            {
                if (speedboosts[i] > 1)
                {
                    speedboosts[i] -= Time.deltaTime * multiplier * 100;
                }
                else
                {
                    speedboosts[i] = 1;
                    currentTime[i] = 0;
                }
            }
            if (speedboosts[i] > maxSpeed)
            {
                speedboosts[i] = maxSpeed;
            }
        }
    }
}

