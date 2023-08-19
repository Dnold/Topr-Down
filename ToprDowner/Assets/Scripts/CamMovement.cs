using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform playerPos;
    public float camSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerDir = playerPos.position - transform.position;
        transform.Translate(playerDir * Time.deltaTime * camSpeed);
    }
}
