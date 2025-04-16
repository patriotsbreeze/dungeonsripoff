using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Player_Movement_Camera : MonoBehaviour
{
    public float speed = 20f;
    public float sens = 10f;
    public float jump = 20f;

    private bool isGround, jumpRequest;
    private float xRot;
    private Vector3 playerDirection;
    private Vector2 mouseInput;
    private Rigidbody rb;
    [SerializeField] Transform camMove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
        Cursor.lockState = CursorLockMode.Locked;

        /* 
            I feel like keeping this off might make the game more fun and unique
        */
        
        //rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float veritcal = Input.GetAxis("Vertical");
        CameraMovement();
        playerDirection = new Vector3(horizontal, 0, veritcal);
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        /*
            In order for the Jump to work, you must 
            make the Ground tag assigned to an object
        */
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            jumpRequest = true;
        }
    }
    void FixedUpdate()
    {
        /*
            character movement is in fixedUpdate to make better use of the physics engine
        */
        Movement();
    if (jumpRequest)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            jumpRequest = false;
        }
    }
    void Movement(){
        Vector3 move = transform.TransformDirection(playerDirection) * speed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }
    void CameraMovement(){
        xRot -= mouseInput.y * sens;

        transform.Rotate(0f, mouseInput.x * sens, 0f);
        camMove.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                isGround = true;
                break;
            }
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
