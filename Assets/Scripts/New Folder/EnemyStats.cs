using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats normalizadas (0-1)")]
    public float hp;
    public float damage;
    public float attackRate;
    public float range;
    public float speed;

    [Header("Cálculo de dificultad")]
    public float difficultyScore;
    public float balanceScore;
    public float totalScore;

    private Renderer enemyRenderer;

    private void Start()
    {
        // Obtener el componente Renderer del enemigo para aplicar colores
        enemyRenderer = GetComponent<Renderer>();
    }

    public void GenerateRandomStats()
    {
        // Stats aleatorias
        hp = Random.value * 100;          // Vida de 0 a 100
        damage = Random.value * 20;      // Daño de 0 a 20
        attackRate = Random.value * 2;   // Tasa de ataque de 0 a 2
        range = Random.value * 15;       // Rango de 0 a 15
        speed = Random.value * 5;        // Velocidad de 0 a 5

        // Llamar a la función para calcular las puntuaciones de dificultad y balance
        CalculateScores();

        // Aplicar el color basado en las estadísticas
        ApplyColorBasedOnStats();
    }

    void CalculateScores()
    {
        // Difficulty = promedio simple
        difficultyScore = (hp + damage + attackRate + range + speed) / 5f;

        // Balance: cuánto se alejan del punto ideal (2.5 en total si todos fueran 0.5)
        float sum = hp + damage + attackRate + range + speed;
        balanceScore = 1f - Mathf.Clamp(Mathf.Abs(sum - 2.5f), 0f, 1f);

        // Total Score (con pesos default, modificables desde fuera si quieres)
        float difficultyWeight = 0.5f;
        float balanceWeight = 0.5f;
        totalScore = difficultyScore * difficultyWeight + balanceScore * balanceWeight;
    }

    void ApplyColorBasedOnStats()
    {
        if (enemyRenderer != null)
        {
            // Creamos una nueva instancia del material para que los cambios no afecten a otros enemigos
            Material enemyMaterial = new Material(enemyRenderer.material);

            // Normalizamos las estadísticas
            float hpNormalized = Mathf.InverseLerp(0f, 100f, hp);
            float damageNormalized = Mathf.InverseLerp(5f, 20f, damage);
            float speedNormalized = Mathf.InverseLerp(1f, 5f, speed);
            float attackRateNormalized = Mathf.InverseLerp(0.1f, 2f, attackRate);
            float rangeNormalized = Mathf.InverseLerp(5f, 15f, range);

            // Interpolación de colores para cada variable
            Color hpColor = Color.Lerp(Color.red, Color.green, hpNormalized);             // Rojo (baja vida) -> Verde (alta vida)
            Color damageColor = Color.Lerp(new Color(1f, 0.5f, 0f), Color.red, damageNormalized);  // Naranja (bajo daño) -> Rojo (alto daño)
            Color speedColor = Color.Lerp(Color.blue, Color.yellow, speedNormalized);    // Azul (baja velocidad) -> Amarillo (alta velocidad)
            Color attackRateColor = Color.Lerp(new Color(0.5f, 0f, 0.5f), Color.magenta, attackRateNormalized); // Púrpura (baja tasa de ataque) -> Rosa (alta tasa de ataque)
            Color rangeColor = Color.Lerp(new Color(0.2f, 0.8f, 0.2f), new Color(0f, 0.5f, 0f), rangeNormalized); // Verde claro (baja distancia) -> Verde oscuro (alta distancia)

            // Mezclamos los colores de las diferentes estadísticas
            Color finalColor = (hpColor + damageColor + speedColor + attackRateColor + rangeColor) / 5f;

            // Asignamos el color final al material
            enemyMaterial.color = finalColor;

            // Aplicamos la instancia del material al objeto
            enemyRenderer.material = enemyMaterial;
        }
    }
}
