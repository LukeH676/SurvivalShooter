using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);// don't need a time for something thats goign to repeat - calls spawn function - Amount of time to wait before spawn - amount fo time to wait after spawn
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)//if player is dead don't spawn - if allive spawn
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length); // random number up to the maximum length

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation); // createa an instance of something - thing to spawn - where to spawn - what rotation it hsould have
    }
}
