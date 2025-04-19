using System.Collections;
using UnityEngine;

public class ChasingSaw : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRange = 5f;
    public float knockbackForce = 11f;
    private Transform player;
    private bool isRetreating = false;
    private Vector2 retreatDirection;
    private Vector2 retreatTarget;
    public float retreatDistanceMultiplier = 0.95f; // 95% от detectionRange


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        transform.Rotate(0, 0, 200 * Time.deltaTime);

        if (player == null) return;

        if (isRetreating)
        {
            // Двигаемся назад
            transform.position = Vector2.MoveTowards(transform.position, retreatTarget, speed * Time.deltaTime);

            // Проверка: достигли ли точки отступления
            if (Vector2.Distance(transform.position, retreatTarget) < 0.1f)
            {
                isRetreating = false; // Возврат к обычному поведению
            }
        }
        else
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= detectionRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                other.gameObject.GetComponent<PlayerController>().getDamage(knockbackDirection, knockbackForce);
                playerHealth.TakeDamage(1f);

                // Запуск отступления
                retreatDirection = (transform.position - player.position).normalized;
                retreatTarget = (Vector2)transform.position + retreatDirection * (detectionRange * retreatDistanceMultiplier);

                isRetreating = true;
            }
        }
    }
}