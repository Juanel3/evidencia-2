using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefabricado del enemigo
    public float spawnInterval = 2f; // Intervalo de tiempo entre cada spawn
    public Transform[] spawnPoints; // Puntos donde aparecerán los enemigos
    public int maxEnemies = 5; // Número máximo de enemigos que se pueden generar
    private int currentEnemyCount = 0; // Contador de enemigos generados

    private void Start()
    {
        if (enemyPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Error: No se asignaron correctamente las variables en el Inspector.");
            return;
        }

        // Comienza a instanciar enemigos después de un intervalo inicial
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies) // Si hemos alcanzado el límite de enemigos, no instanciamos más
        {
            CancelInvoke("SpawnEnemy"); // Detenemos el spawner si ya hemos generado todos los enemigos
            Debug.Log("Se alcanzó el máximo de enemigos.");
            return;
        }

        if (spawnPoints.Length == 0) return; // Si no hay puntos de spawn, no hace nada

        // Elige un punto de spawn aleatorio
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instancia el enemigo en el punto seleccionado
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Incrementa el contador de enemigos
        currentEnemyCount++;
    }
}
