using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    public string doorColor; // Цвет двери, например "red", "blue"
    public Transform destination; // Куда телепортировать игрока (назначается автоматически)
    public float cooldownTime = 3.5f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && destination != null)
        {
            PlayerController teleportState = other.GetComponent<PlayerController>();
            if (teleportState != null && !teleportState.recentlyTeleported)
            {   
                AudioManager.instance.PlaySFX("Door");
                Vector3 offset = new Vector3(0, 0.7f, 0); // Подними игрока на 0.5 единиц
                other.transform.position = destination.position + offset;

                teleportState.SetTeleportCooldown(cooldownTime); // сколько секунд игнорировать телепорт
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