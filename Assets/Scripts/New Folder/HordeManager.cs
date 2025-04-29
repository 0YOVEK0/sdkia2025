using UnityEngine;

public class HordeManager : MonoBehaviour
{
    [Header("Configuración de la horda")]
    public GameObject enemyPrefab;
    public int enemigosPorRonda = 5;

    [Header("LayerMask")]
    public LayerMask floorLayer;  // Capa del suelo

    private void Start()
    {
        GenerarHorda();
    }

    public void GenerarHorda()
    {
        // Limpiar enemigos de rondas anteriores
        LimpiarHorda();

        // Obtener todos los colliders de los objetos con la capa 'floorLayer' en la escena
        Collider[] floorColliders = Physics.OverlapSphere(Vector3.zero, 1000f, floorLayer);  // Asegúrate de ajustar el radio a tus necesidades

        for (int i = 0; i < enemigosPorRonda; i++)
        {
            // Elegir un collider aleatorio de los objetos con la capa 'floorLayer'
            Collider selectedCollider = floorColliders[Random.Range(0, floorColliders.Length)];

            // Generar una posición aleatoria dentro de este collider
            Vector3 spawnPosition = new Vector3(
                Random.Range(selectedCollider.bounds.min.x, selectedCollider.bounds.max.x),
                selectedCollider.bounds.center.y,  // Usar el centro Y del collider
                Random.Range(selectedCollider.bounds.min.z, selectedCollider.bounds.max.z)
            );

            // Usar Raycast para asegurarnos que el enemigo aparece sobre el suelo
            RaycastHit hit;
            if (Physics.Raycast(spawnPosition + Vector3.up * 10, Vector3.down, out hit, 20f, floorLayer))
            {
                spawnPosition = hit.point;  // Ajustar la posición del spawn sobre el suelo

                // Instanciar el enemigo en la posición ajustada
                GameObject nuevoEnemigo = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                // Generar estadísticas aleatorias para el enemigo
                EnemyStats stats = nuevoEnemigo.GetComponent<EnemyStats>();
                stats.GenerateRandomStats(); // Llamar a la función para generar stats aleatorias
            }
        }
    }

    // Limpiar enemigos actuales de la horda
    private void LimpiarHorda()
    {
        // Aquí podemos agregar la lógica para eliminar enemigos existentes si es necesario
    }
}
