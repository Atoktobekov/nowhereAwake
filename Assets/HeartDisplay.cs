using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    [SerializeField] private Image[] hearts; // Привяжем 3 сердечка
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    private Health playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<Health>(); // Можно заменить на прямую ссылку, если нужно
        UpdateHearts();
    }

    private void Update()
    {
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
