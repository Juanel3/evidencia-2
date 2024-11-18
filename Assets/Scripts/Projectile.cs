using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Velocidad del proyectil
    public int damage = 1;    // Daño que hace el proyectil
    public float lifetime = 5f; // Tiempo de vida del proyectil antes de desaparecer

    private Rigidbody2D rb; // Referencia al Rigidbody2D para controlar el movimiento

    void Start()
    {
        // Obtener el Rigidbody2D y asignar la velocidad de movimiento
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; // El proyectil se mueve hacia la derecha

        // Destruir el proyectil después de cierto tiempo
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el proyectil colisiona con un enemigo, hace daño
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("¡El proyectil tocó al enemigo!"); // Para verificar la colisión

            // Llama al método TakeDamage en el enemigo para reducir su salud
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);

            // Destruye el proyectil después de hacer el daño
            Destroy(gameObject);
        }

        // Si el proyectil toca el suelo o algo que no sea un enemigo, también se destruye
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
