using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;


public class Health : MonoBehaviour
{
    [SerializeField] public float startHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private Vector3 respawnPoint;
    public float cameraMoveDuration = 0.5f; // Время плавного возврата камеры
    public CinemachineCamera virtualCamera; // Cinemachine камера

    private PlayerController player; 
    public GameObject gameOverPanel;


    [SerializeField] private GameObject effect;
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [FormerlySerializedAs("CapsuleCollider")] [SerializeField] private CapsuleCollider2D capsuleCollider;
    

    private SpriteRenderer spriteRend;

    bool dead;

    private void Start()
    {
        respawnPoint = transform.position;

    }

    private void Awake()
    {
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerController>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bottom"))
        {
            TakeDamage(1f);
            StartCoroutine(DieFromCringe());
        }   
    }

    public void TakeDamage(float _damage)
    {   
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startHealth);

        if(currentHealth > 0)
        {   
            Debug.Log($"Health: {currentHealth}");
            StartCoroutine(Invunerability());
        }
        else 
        {
            if (!dead)
            {
                StartCoroutine(Die());
            }

        }

    }

    private IEnumerator Invunerability() 
    {
        Physics2D.IgnoreLayerCollision(3, 6, true);
        for(int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(3, 6, false);

    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startHealth);
    }

    public void RespawnHealth()
    {
        dead = false;
        AddHealth(startHealth);
        //anim.ResetTrigger("die");
        anim.Play("PlayerIdle");
        startHealth = 3;
    }
    
    public void SetCheckpoint(Vector3 checkPoint)
    { 
        //AudioManager.instance.PlaySFX("checkpoint");
        respawnPoint = checkPoint;
    }
    
    
    private IEnumerator Die()
    {
        //AudioManager.instance.PlaySFX("die");
        player.SetCanMove(false); 
        dead = true;
        anim.SetTrigger("Die");

        yield return new WaitForSeconds(0.6f);
    
        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
            gameOverPanel.SetActive(true); // Показать панель
            Time.timeScale = 0f; // Остановить игру
            yield break; 
        }
    }
    
    private IEnumerator DieFromCringe()
    {
        //AudioManager.instance.PlaySFX("die");
        player.SetCanMove(false); 
        dead = true;
        anim.SetTrigger("Die");

        yield return new WaitForSeconds(0.6f);
    
        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
            gameOverPanel.SetActive(true); // Показать панель
            Time.timeScale = 0f; // Остановить игру
            yield break; 
        }
        yield return StartCoroutine(Respawn()); 
    }
    
    private IEnumerator Respawn()
    {
        Vector3 oldPosition = transform.position; // Запоминаем старую позицию игрока
        transform.position = respawnPoint; // Устанавливаем игрока в точку респауна
        
        // Плавно двигаем и игрока, и камеру к точке респауна
        yield return StartCoroutine(SmoothCameraAndPlayerReset());

        anim.Play("Appearing"); // Запускаем анимацию появления
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f); // Ждем окончания анимации

        dead = false; // Игрок оживает
        player.SetCanMove(true); // Разрешаем движение игрока
    }
    
    private IEnumerator SmoothCameraAndPlayerReset()
    {
        Transform camTransform = virtualCamera.transform; 
        Vector3 startPosition = camTransform.position; 
        Vector3 targetPosition = new Vector3(respawnPoint.x, respawnPoint.y, camTransform.position.z); 
        float elapsedTime = 0f; 

        while (elapsedTime < cameraMoveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, respawnPoint, elapsedTime / cameraMoveDuration); 
            camTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / cameraMoveDuration); 

            elapsedTime += Time.deltaTime; 
            yield return null; 
        }

        transform.position = respawnPoint;
        camTransform.position = targetPosition;
    }
    
}
