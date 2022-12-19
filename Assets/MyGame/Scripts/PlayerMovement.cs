using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float rotation;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = -Input.GetAxis("Horizontal");

        //rotate the player
        transform.Rotate(0, 0, h * rotation);

        //move the player in the direction it is facing
        rb.AddForce(transform.right * acceleration * v);
    }
}
