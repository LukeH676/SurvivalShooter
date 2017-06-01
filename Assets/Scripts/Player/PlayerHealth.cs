using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement; // reference to player movement script from earlier
    PlayerShooting playerShooting;//reference to playershooting script
    bool isDead;
    bool damaged;


    void Awake () // called as game begins 
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> (); // get component of PLayerMovement script
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update () //updates every scene
    {
        if(damaged)// if we are damaged - set the image to the flashing colour -
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime); // not damaged? stop flashing. Color.lerp is a smooth way to change colours (Current colour, Invisible, deltaTime (smooth transitioning))
        }
        damaged = false; // set damaged to false after taking damage. so the red screen doesn't stay
    }


    public void TakeDamage (int amount) // Not called in this script, will be used in other scripts HAS TO BE PUBLIC
    {
        damaged = true; // makes the red flash show up

        currentHealth -= amount; // curren health - amount of damage = current health

        healthSlider.value = currentHealth; // take the value fo health and change slider

        playerAudio.Play (); // audio source - player hurt

        if(currentHealth <= 0 && !isDead) // if dead - go to death function
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die"); // animator controller (DIE) plays the animation

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false; // access pplayermovement script. set it false - stops player moving
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
