using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float speed = 5f;
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
    }
}
