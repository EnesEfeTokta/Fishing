using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HealthFish : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material damageMaterial;
    [SerializeField] private Material originalMaterial;
    private Renderer rdr;

    [Header("Health")]
    [SerializeField] private float health = 100;
    private float healthValue;
    [SerializeField] private Image healthBarValue;

    [Header("Particles/VFX")]
    [SerializeField] private ParticleSystem blood;

    void Start()
    {
        healthValue = health;

        rdr = GetComponent<Renderer>();
        rdr.material = originalMaterial;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Test
            CauseDamage(50);
        }
    }

    public void CauseDamage(float damage)
    {
        healthValue -= damage;
        healthBarValue.fillAmount = healthValue / 100f;
        StartCoroutine(MaterialChange());
        if (healthValue <= 0)
        {
            FindFirstObjectByType<FishSuccess>().ShowSuccessIcon(transform.position);
            Death();
        }
    }

    IEnumerator MaterialChange()
    {
        rdr.material = damageMaterial;
        blood.Play();
        yield return new WaitForSeconds(0.2f);
        rdr.material = originalMaterial;
    }

    void Death()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spear"))
        {
            CauseDamage(20);
        }
    }
}
