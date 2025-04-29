using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    [Header("Ataque")]
    public Transform attackPoint;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    public LayerMask enemyLayer;

    private float lastAttackTime;

    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(h, 0, v).normalized;
        if (inputDir.magnitude >= 0.1f)
        {
            // Rotar hacia la dirección de la cámara
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            // Mover hacia adelante
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("Golpeaste al enemigo: " + enemy.name);
            // Aquí puedes aplicar daño o efectos
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
