using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIDamage : MonoBehaviour
{
    public TMP_Text text;

    public void DisplayDamage(int amount)
    {
        StartCoroutine(DisplayDamageAnimationCycle(amount));
    }
    IEnumerator DisplayDamageAnimationCycle(int amount)
    {
        text.gameObject.SetActive(true);
        for(int i = 0; i < amount; i++)
        {
            text.text = "-"+i.ToString();
            int dislayPosition = Math.Clamp(i, 0, 2);
            transform.position += new Vector3(0,dislayPosition * Time.deltaTime);
            yield return new WaitForSeconds(0.001f);

        }
        
        text.gameObject.SetActive(false);
    }
}
