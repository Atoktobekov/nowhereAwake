using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isPassed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!isPassed)
            {
                isPassed = true;
                collision.GetComponent<Health>().SetCheckpoint(transform.position);
            }
        }
    }
}
