using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFollow : MonoBehaviour
{
    Player player;
    AIDestinationSetter destinationSetter;
    public CatAnimationController controller;
    public float newWaypointTime;
    public int idleChance = 5;
    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(PickNewWaypoint());
    }
    //hanhalol
    IEnumerator PickNewWaypoint()
    {
        while(true)
        {
            if(Random.Range(0, idleChance)+1 != idleChance)
            {
                GameObject tempPoint = Instantiate(new GameObject(), RandomPointOnXYCircle(player.transform.position, 2f), Quaternion.identity);
                destinationSetter.target = tempPoint.transform;
                yield return new WaitForSeconds(newWaypointTime);
                Destroy(tempPoint);
            }
            else
            {
                controller.PlayIdleAnimation();
                yield return new WaitForSeconds(newWaypointTime*2);
            }
        }
    }
    Vector3 RandomPointOnXYCircle(Vector2 center, float radius)
    {
        float angle = Random.Range(0, 2f * Mathf.PI);
        return center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
    }
}
