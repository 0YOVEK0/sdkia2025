using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 300f;
    public int maxHealth = 100;
    private int currentHealth;
    private bool isImmortal = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ) * speed;
        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);  // Corregir la variable para usar 'velocity'
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    public void TakeDamage(float damage)
    {
        if (!isImmortal) // Solo recibe daño si no es inmortal
        {
            currentHealth -= Mathf.RoundToInt(damage);  // Redondear el daño a un valor entero
            Debug.Log("Player HP: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Debug.Log("Player died");
        gameObject.SetActive(false);  // El jugador se desactiva cuando muere
    }

    public void ToggleImmortality()
    {
        isImmortal = !isImmortal;
        Debug.Log("Inmortalidad: " + isImmortal);
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
