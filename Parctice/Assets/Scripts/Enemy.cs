using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public int hp = 100;
    public NavMeshAgent agent;
    
    public Transform player;
    
    bool moving;
  
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
        } 

        if (moving)
            agent.SetDestination(player.position);



        if (hp <= 0){
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) 
    {
        hp -= damage;
    }
}
