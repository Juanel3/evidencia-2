using UnityEngine;

public class AlienHealth : MonoBehaviour
{
    public int maxHealth = 10; // Vida máxima del alien
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Inicializamos la vida al máximo
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Vida actual del alien: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("El alien ha muerto.");
        // Aquí podrías reiniciar el nivel, mostrar pantalla de Game Over, etc.
    }
}
