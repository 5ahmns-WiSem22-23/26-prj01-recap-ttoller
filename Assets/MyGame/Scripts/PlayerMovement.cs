using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float rotation;
    [SerializeField]
    private float defenseForceMultiplier = 30;
    [SerializeField]
    private float pushBackForce = 150;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Speed");
            rb.AddForce(-transform.up * acceleration * defenseForceMultiplier);
        }
    }
    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = -Input.GetAxis("Horizontal");

        //rotate the player
        transform.Rotate(0, 0, h * rotation);

        //move the player in the direction it is facing
        rb.AddForce(-transform.up * acceleration * v);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && Input.GetKey(KeyCode.Space))
        {
            Vector2 pushBackDirection = (transform.position - other.transform.position).normalized;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(-pushBackDirection * pushBackForce);
        }
    }
}
