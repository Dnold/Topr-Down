using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float attackTime;

    bool canAttack = true;
    public AnimationController animController;
    public Transform attackPoint;
    public float attackRange;
    public int damage;

    public Player player;

    public float attackAngle;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);        
    }
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }
    }
    IEnumerator AttackTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }
    void Attack()
    {
        animController.PlayAttackAnimation();
        StartCoroutine(AttackTimer());
        player.attackAudioSource.clip = player.clips[0];
        player.attackAudioSource.Play();

        Collider2D[] hitEnemiesCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        Enemy[] hitEnemies = hitEnemiesCollider.Where(e => e.tag == "Enemy").Select(e => e.GetComponent<Enemy>()).ToArray();

        foreach(Enemy enemy in hitEnemies)
        {
            int givenDamage = damage;
            
            Vector2 attackDir = (enemy.transform.position - transform.position).normalized;
            Debug.Log(attackDir);
            enemy.GetHit(attackDir, givenDamage);
        }


    }
}
