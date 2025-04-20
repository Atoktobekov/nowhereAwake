using System;
using UnityEngine;

public class BlackDoor : MonoBehaviour
{
    public GameObject finishPanel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishPanel.SetActive(false);
        }
    }
}
