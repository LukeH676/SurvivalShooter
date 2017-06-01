using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 25;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    float timer; // keeps everything in sync - only attack when supposed to
    Ray shootRay = new Ray();// Raycast out to find what you hit
    RaycastHit shootHit; // returns back whatever we hit
    int shootableMask;// only hit shootable things
    ParticleSystem gunParticles;// particle component to gun
    LineRenderer gunLine;// 
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;// how long effects are viewable


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");// shootable mask "Shootable" returns number of shootable layer
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime) // if enugh time has happened since firing - disable effects
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;// turns on line renderer
        gunLine.SetPosition (0, transform.position); // sets position of the line at the barrel of the gun - 

        shootRay.origin = transform.position; // start position
        shootRay.direction = transform.forward;// shoots ray forward

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask)) //Give us information as to what we hit (shoothit)/ RANGE - 100F 100 units /Shootablemask - Can only hit shootable things
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> (); // get the enemy health script - 
            if(enemyHealth != null) // if the enemy health scrip does not = null - so if the hitting point is an enemy - enemy takes damage
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);// if it hits something shot is going to disapear
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);// if we don't hit anything (
        }
    }
}
