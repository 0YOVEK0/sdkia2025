using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Prefab del enemigo
    public int numberOfEnemies = 5;      // Número de enemigos a generar
    public float spawnRange = 10f;       // Rango de aparición en el plano
    public float planeHeight = 0f;       // Altura sobre el plano (normalmente 0 si el plano está en el suelo)

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Generar una posición aleatoria dentro del rango del plano
            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);
            Vector3 spawnPosition = new Vector3(randomX, planeHeight, randomZ);

            // Instanciar el enemigo en la posición generada
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Establecer valores aleatorios para el enemigo
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.health = Random.Range(50f, 300f);  // HP aleatorio
            enemyController.attackDamage = Random.Range(5f, 20f); // Daño aleatorio
            enemyController.attackRate = Random.Range(1f, 3f);  // Tasa de ataque aleatoria
            enemyController.moveSpeed = Random.Range(1f, 5f);   // Velocidad aleatoria
        }
    }
}
