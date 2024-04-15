using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;
    public MenuController menuController;

    public KeyCode w;
    public KeyCode a;
    public KeyCode s;
    public KeyCode d;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    Vector3 velocity;

    public Transform cam;

    public bool isPoweredUp;
    public float powerBounceStrength;
    public float powerUpTime = 7f;

    public Transform respawnPoint;
    private float playerlifecount = 3f;

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.5f);
    }



    private void Awake()
    {
        //references within own object

        playerRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //references to other objects
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump")) 
        {
            Jump();
        }

        if(transform.position.y < -15) 
        {
            if(playerlifecount > 0) {
                 Respawn();
                playerlifecount --;
            }
            else {
                EndGame();
            }
           
        }
    }


    private void FixedUpdate()
    {
        //calls every 3 frames
        //for physics calculations

       /* float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        // x axis goes from -1 to +1
        //Horizontal = X, Vertical = Z, Up = Y

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); */
        //which direction applied

        //playerRB.AddForce(movement * speed * Time.deltaTime);
        //Time.delatime is time in game, based on frame rate

         if (Input.GetKey(a))
        {
            playerRB.AddForce(Vector3.left * speed);
        }
 
        if (Input.GetKey(d))
        {
            playerRB.AddForce(Vector3.right * speed);
        }
 
        if (Input.GetKey(w))
        {
            playerRB.AddForce(Vector3.forward * speed);
        }
 
        if (Input.GetKey(s))
        {
            playerRB.AddForce(Vector3.back * speed);
        }

        /* if(movement.magnitude > 0.1f) {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            playerRB.AddForce(moveDir * speed * Time.deltaTime);
        } */
    }

    private void Jump()
    {
        if(isGrounded() == true)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //collider, but doesn't have substance, not physical

        if(other.CompareTag("Powerup"))
        {
            isPoweredUp = true;
            Destroy(other.gameObject);
           StartCoroutine(powerUpCountDownRoutine());
        }
    }

    IEnumerator powerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(powerUpTime);
        isPoweredUp = false;
    } 

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    void Respawn()
    {
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        playerRB.Sleep();
        transform.position = respawnPoint.position;
    }

    void EndGame() {
        menuController.LoseGame();
        gameObject.SetActive(false);
    }

}
