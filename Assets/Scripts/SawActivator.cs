using UnityEngine;

public class SawActivator : MonoBehaviour
{
    public GameObject Saw; 
    public float delay = 3.5f; 

    void Start()
    {
        Saw.SetActive(false); 
        StartCoroutine(ActivateSawAfterDelay());
    }

    System.Collections.IEnumerator ActivateSawAfterDelay()
    {
        yield return new WaitForSeconds(delay); 
        Saw.SetActive(true); 
    }
}