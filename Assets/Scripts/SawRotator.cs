using UnityEngine;

public class SawRotator : MonoBehaviour
{
    public Transform centerPoint;
    public float radius = 2f;
    public float rotationSpeed = 90f;

    private float angle;

    void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;
        transform.position = centerPoint.position + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
    }
}