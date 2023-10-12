using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� �������� ���������
    public GameObject projectilePrefab; // ������ �������
    private Vector2 moveInput = Vector2.zero; // ���� ��� ��������
    private float shootCooldown = 0.5f; // ����� ����� ����������
    private float nextShootTime = 0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Time.time >= nextShootTime && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Shoot(Vector2.up);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Shoot(Vector2.down);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Shoot(Vector2.left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Shoot(Vector2.right);
            }
        }
    }

    private void FixedUpdate()
    {
        // ��������� ������ �������� � Rigidbody ��������� ��� ������� WASD
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            moveInput = Vector2.zero;
            rb.velocity = Vector2.zero;
        }
        else
        {
            moveInput = new Vector2(Input.GetKey(KeyCode.D) ? 1f : (Input.GetKey(KeyCode.A) ? -1f : 0f),
                Input.GetKey(KeyCode.W) ? 1f : (Input.GetKey(KeyCode.S) ? -1f : 0f));
            moveInput.Normalize();
            rb.velocity = moveInput * moveSpeed;
        }
    }

    private void Shoot(Vector2 direction)
    {
        if (Time.time >= nextShootTime)
        {
            // ������������ ��������� ������� ������� ������ ���������
            Vector2 spawnPosition = (Vector2)transform.position + direction * 0.1f; // �������� �������� 0.1f �� ������ ����������

            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            // ���������� �������� � �������
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            projectileRb.velocity = direction * moveSpeed * 2f;

            nextShootTime = Time.time + shootCooldown;
        }
    }

}
