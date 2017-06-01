using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth; // reference to player health script
    EnemyHealth enemyHealth;
    bool playerInRange; // goes to true when enemy is close enough to player to attack
    float timer;// keeps everything in sync


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player"); // find a game object ("Finds this object") innefcient call?
        playerHealth = player.GetComponent <PlayerHealth> (); // 
        enemyHealth = GetComponent<EnemyHealth>(); // get the enemy health from enemyhealth script
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)// if the object running into enemy is a player - playerinrange = true
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player) // If the item leaving the trigger (area around enemy) set playerinrange to false
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime; // accumulates time - represents how much time has apssed 

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) // if its been long enough - attack player - if not dont
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0) // enemy attack killed player - plays the animation "PlayerDead"
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
