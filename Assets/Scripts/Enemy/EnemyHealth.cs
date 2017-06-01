using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f; // hwo fast dead enemies sink into floor
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> (); // find the child inside of particleSystem
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime); // checks if enemy is sinking - if it is - move it down(-vector) by the sink speed per second (delta time)
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint; // selects the position to play the particle  - Fluff coming out
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead"); // performs its dead animation


        enemyAudio.clip = deathClip; // change to death clip
        enemyAudio.Play ();
    }


    public void StartSinking () // on an animation event - 
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false; // not turn off the whole game object. just ONE component of a game object
        GetComponent <Rigidbody> ().isKinematic = true; // when you move colider in scene - tries to recalculate the scene - Kinematic rigid body ignores the resyncing
        isSinking = true;
        //ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
