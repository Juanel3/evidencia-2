using UnityEngine;

public class AlienController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public float jumpForce = 5f; // Fuerza de salto
    public Transform firePoint; // El punto donde se generarán los disparos
    public GameObject laserPrefab; // El prefab del láser
    public int health = 5; // Salud del alien

    private Rigidbody2D rb;
    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Si no se asignó un firePoint en el Inspector, lo asignamos a un punto relativo
        if (firePoint == null)
        {
            firePoint = new GameObject("FirePoint").transform;
            firePoint.SetParent(transform); // Lo hacemos hijo del alien
            firePoint.localPosition = new Vector3(1f, 0f, 0f); // Ajusta la posición relativa
        }
    }

    void Update()
    {
        Move();
        Jump();
        FireLaser();
    }

    void Move()
    {
        // Movimiento horizontal
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    void Jump()
    {
        // Salto
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void FireLaser()
    {
        // Disparar láser
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instanciamos el láser desde el firePoint
            Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detectar colisiones con enemigos y restar vida
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 1;
            Debug.Log("Vida del alien: " + health);
            
            // Si la vida llega a cero, el alien desaparece
            if (health <= 0)
            {
                Destroy(gameObject);
                Debug.Log("El alien ha muerto.");
            }
        }

        // Detectar si el alien aterriza después de saltar
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // El alien ya no está en el aire
        }
    }
}
