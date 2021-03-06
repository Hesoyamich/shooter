using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public int hp = 100;
    public int damage = 20;
    public int moneyForKill = 100;
    public NavMeshAgent agent;

    Transform player;
    SpawnerController spawnContoll;
    //

    //
    public LayerMask playerLayerMask;
    public float attackCooldown;
    bool moving;
    bool attacking;

    public Transform weapon;
    Vector3 weaponPos;

    void Start() {
        player = GameObject.Find("PlayerPrefab").transform.GetChild(0);
        spawnContoll = GameObject.Find("GameController").GetComponent<SpawnerController>();
        weaponPos = weapon.localPosition;
    }

    void Update()
    {
        weapon.localPosition = weaponPos;
        if (Vector3.Distance(player.position, transform.position) > 2f)
            moving = true;
        
        else
        {
            moving = false;
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            if (!attacking)
                StartCoroutine(AttackPlayer());
        } 

        if (moving && !attacking)
            agent.SetDestination(player.position);

        if (hp <= 0){
            Death();
        }
    }

    public void TakeDamage(int damage) 
    {
        hp -= damage;
    }

    IEnumerator AttackPlayer()
    {
        
        attacking = true;
        player.gameObject.GetComponent<PlayerMovement>().GetDamage(damage);
        Debug.Log(player.gameObject.GetComponent<PlayerMovement>().hp);
        weaponPos += new Vector3(0,0,1);
        yield return new WaitForSeconds(attackCooldown);
        weaponPos -= new Vector3(0,0,1);
        attacking = false;
        
    }

    void Death()
    {
        spawnContoll.MinusCurEnemy();
        spawnContoll.AddKilledEnemy();
        player.gameObject.GetComponent<PlayerMovement>().money += moneyForKill;
        // Debug.Log(player.gameObject.GetComponent<PlayerMovement>().money);
        Destroy(gameObject);
        //        
        player.gameObject.GetComponent<PlayerMovement>().killSum += 1;
    }
}
