using System;
using UnityEngine;

public class ChasingSawDeactivator : MonoBehaviour
{
    
    public GameObject Saw1; 
    public GameObject Saw2; 
    public GameObject Saw3; 
    public GameObject Saw4; 
    public GameObject Saw5; 
    
    void Start()
    {
        Saw1.SetActive(true); 
        Saw2.SetActive(true); 
        Saw3.SetActive(true); 
        Saw4.SetActive(true); 
        Saw5.SetActive(true); 

    }

    public void DeactivateSaws()
    {
        Saw1.SetActive(false); 
        Saw2.SetActive(false); 
        Saw3.SetActive(false); 
        Saw4.SetActive(false); 
        Saw5.SetActive(false); 
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX("Off");
            DeactivateSaws();
        }
    }
    
}
