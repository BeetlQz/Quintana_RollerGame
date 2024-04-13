using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    Vector3 velocity;

    public Transform cam;

    public bool isPoweredUp;
    public float powerBounceStrength;
    public float powerUpTime = 7f;

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
    }

    private void FixedUpdate()
    {
        //calls every 3 frames
        //for physics calculations

        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        // x axis goes from -1 to +1
        //Horizontal = X, Vertical = Z, Up = Y

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //which direction applied

        //playerRB.AddForce(movement * speed * Time.deltaTime);
        //Time.delatime is time in game, based on frame rate

        if(movement.magnitude > 0.1f) {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            playerRB.AddForce(moveDir * speed * Time.deltaTime);
        }
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
        if(collision.gameObject.CompareTag("Enemy") && isPoweredUp == true)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 bounceDir = (collision.gameObject.transform.position - transform.position);
            enemyRB.AddForce(bounceDir * powerBounceStrength, ForceMode.Impulse);
        }
    }

}
