using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float maxVelocityTolerance = 100f;
    private Rigidbody2D rb;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        rb.AddForce(transform.right * speed);
        if (rb.velocity.magnitude > 20) Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude > maxVelocityTolerance)
        {
            GetComponent<ParticleSystem>().Play();
            Destroy(gameObject, 0.5f);
        }
    }
}
