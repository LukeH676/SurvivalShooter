using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidBody;
    int floorMask; // Remember floor quad? Ray Casting into it (Remember zigzag? little ray underneath character)
    float camRayLength = 100f; // this is the Ray (like zigzag)

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor"); // layer that we are GETing (floor quad is callede FLoor)
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()//automatically called by Unity
    {
        float h = Input.GetAxisRaw("Horizontal"); //Horizontal input - 
        float v = Input.GetAxisRaw("Vertical");// vertical input
        Move(h, v);
        Turning();
        Animating(h, v);
   
    }
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v); // XYZ - Y is up and down - thats why its 0
        movement = movement.normalized * speed * Time.deltaTime; // time deltaTime is time between each update call - 
        playerRigidBody.MovePosition(transform.position + movement);// move him by current position + movement
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); // points the ray at the MOUSE POSITION
        RaycastHit floorHit; 
        if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse); //default for characters and cameras - Z axis is forward axis - we want to make player to mouse as forward function
            playerRigidBody.MoveRotation(newRotation);
    
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f; // h is that not equal to 0? (returns true or false) OR is V Not equal to 0? Did we press horizontal or vertical access?
        anim.SetBool("IsWalking",walking);

    }
}
