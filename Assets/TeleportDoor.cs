using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    public string doorColor; // Цвет двери, например "red", "blue"
    public Transform destination; // Куда телепортировать игрока (назначается автоматически)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && destination != null)
        {
            PlayerController teleportState = other.GetComponent<PlayerController>();
            if (teleportState != null && !teleportState.recentlyTeleported)
            {
                other.transform.position = destination.position;
                teleportState.SetTeleportCooldown(3.5f); // сколько секунд игнорировать телепорт
            }
        }
    }


    // Этот метод вызывается при запуске, чтобы найти подходящую дверь
    private void Start()
    {
        TeleportDoor[] allDoors = FindObjectsOfType<TeleportDoor>();
        foreach (TeleportDoor door in allDoors)
        {
            if (door != this && door.doorColor == this.doorColor)
            {
                destination = door.transform;
                break;
            }
        }
    }
}