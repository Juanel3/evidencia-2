using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target; // El alien
    public float speed = 3f; // Velocidad de movimiento
    public float detectionRadius = 5f; // Radio de detección
    public int health = 3; // Salud inicial del enemigo

    private void Start()
    {
        // Buscar al jugador por su etiqueta si no se asignó en el Inspector
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (target == null)
            {
                Debug.LogError("No se encontró un objeto con la etiqueta 'Player'.");
                return;
            }
        }
    }

    private void Update()
    {
        // Verifica si el jugador está dentro del radio de detección y lo sigue
        FollowTarget();
    }

    // Método para mover al enemigo hacia el objetivo
    void FollowTarget()
    {
        if (target == null) return;

        // Verifica si el jugador está dentro del radio de detección
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= detectionRadius)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        health -= damage; // Restar la cantidad de daño
        Debug.Log("Salud del enemigo después de recibir daño: " + health); // Mostrar en consola

        // Si la salud llega a cero o menos, destruir al enemigo
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("El enemigo ha sido destruido.");
        }
    }

    // Visualiza el radio de detección en el Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
