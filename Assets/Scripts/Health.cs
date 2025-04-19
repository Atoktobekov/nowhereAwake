using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


public class Health : MonoBehaviour
{
    [SerializeField] public float startHealth;
    public float currentHealth { get; private set; }
    private Animator anim;

    [SerializeField] private GameObject effect;
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [FormerlySerializedAs("CapsuleCollider")] [SerializeField] private CapsuleCollider2D capsuleCollider;
    

    private SpriteRenderer spriteRend;

    bool dead;

 
    private void Awake()
    {
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
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
               Debug.Log("Dead");
                if(GetComponent<PlayerController>() != null)
                    GetComponent<PlayerController>().SetCanMove(false);
              
                dead = true;
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
    
}
