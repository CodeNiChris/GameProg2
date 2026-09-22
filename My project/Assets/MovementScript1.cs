using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float jumpForce = 150f;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float vaxis = Input.GetAxis("Vertical");
        float haxis = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            // Movement
            Vector3 moveDir = transform.TransformDirection(new Vector3(vaxis, 0, haxis));
            rb.AddForce(moveDir * moveSpeed);

            // Rotation
            if (vaxis != 0 || haxis != 0)
            {
                transform.Rotate(new Vector3(0, haxis * rotationSpeed, 0));
            }
        }
        else
        {
            rb.AddForce(new Vector3(0, 0, -moveSpeed * 0.7f)); // Air damping
        }

        // Jump
        if ((Input.GetButton("Jump") || Input.GetKey(KeyCode.Joystick1Button0)) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isJumping = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}