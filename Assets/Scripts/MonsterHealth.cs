
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public float knockbackForce = 9.35f;
    private bool isDead = false;
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && !isDead)
        {
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            Health playerHealth = coll.gameObject.GetComponent<Health>();
            
                Vector2 knockbackDir = (transform.position.x > coll.transform.position.x) ? Vector2.left : Vector2.right;
                player.getDamage(knockbackDir, knockbackForce);
                playerHealth.TakeDamage(1f);
                
        }
    }
}