using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public int hp = 100;
    public int damage = 20;
    public NavMeshAgent agent;
    
    public Transform player;
    
    public LayerMask playerLayerMask;
    public float attackCooldown;
    bool moving;
    bool attacking;

    void Awake() {

    }

    void Update()
    {
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
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) 
    {
        hp -= damage;
    }

    IEnumerator AttackPlayer()
    {
        if (Physics.CheckSphere(transform.position + transform.forward, 1f, playerLayerMask))
        {
            attacking = true;
            player.parent.gameObject.GetComponent<PlayerMovement>().GetDamage(damage);
            Debug.Log(player.parent.gameObject.GetComponent<PlayerMovement>().hp);
            yield return new WaitForSeconds(attackCooldown);
            attacking = false;
        }
    }
}
