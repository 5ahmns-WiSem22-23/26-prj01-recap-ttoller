using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float maxVelocityTolerance = 100f;
    private Rigidbody2D rb;
    private StarManager starManager;
    private bool isDestroyed = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        starManager = GameObject.FindGameObjectWithTag("StarManager").GetComponent<StarManager>();
    }

    void Update()
    {
        transform.LookAt(player.transform.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        rb.AddForce(transform.right * speed);
        if (rb.velocity.magnitude > maxVelocityTolerance)
        {
            GetComponent<ParticleSystem>().Play();
            if (!isDestroyed) starManager.EnemyCarDestroyed();
            isDestroyed = true;
            Destroy(gameObject, 0.5f);
        }
    }
}
