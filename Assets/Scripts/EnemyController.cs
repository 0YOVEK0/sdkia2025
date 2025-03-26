using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f;
    public float attackDamage = 10f;
    public float attackRate = 1f;
    public float moveSpeed = 3f;
    private float nextAttackTime = 0f;
    private Renderer enemyRenderer;
    private Color originalColor;

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color;

        // Establecer la dificultad del enemigo
        SetEnemyDifficulty();
    }

    void Update()
    {
        MoveTowardsPlayer();

        if (Time.time >= nextAttackTime)
        {
            // Aquí puedes implementar la lógica de ataque al jugador
            AttackPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Aquí puedes implementar la lógica de movimiento, por ejemplo, que el enemigo se acerque al jugador
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void AttackPlayer()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            // Aplica daño al jugador
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Función para cambiar el color según la dificultad
    void SetEnemyDifficulty()
    {
        float difficulty = CalculateDifficulty();

        // Debug para ver el valor de la dificultad
        Debug.Log("Dificultad: " + difficulty);

        // Cambiar color dependiendo de la dificultad
        if (difficulty < 200f)
        {
            enemyRenderer.material.color = Color.green; // Fácil
        }
        else if (difficulty < 400f)
        {
            enemyRenderer.material.color = Color.yellow; // Medio
        }
        else
        {
            enemyRenderer.material.color = Color.red; // Difícil
        }
    }

    // Función de evaluación de dificultad (fitness function)
    float CalculateDifficulty()
    {
        // Aumentar el impacto de la salud
        float normalizedHealth = Mathf.Clamp(health / 50f, 1f, 5f); // Reducir el rango de la salud para que afecte más
        float normalizedAttackDamage = Mathf.Clamp(attackDamage / 5f, 1f, 5f); // Ajustar el rango de daño
        float normalizedAttackRate = Mathf.Clamp(1f / attackRate, 1f, 5f); // Ajustar la tasa de ataque

        // Calcular la dificultad ajustada con mayor ponderación
        return normalizedHealth * 65f + normalizedAttackDamage * 20f + normalizedAttackRate * 10f;
    }
}
