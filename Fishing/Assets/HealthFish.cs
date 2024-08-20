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
            CauseDamage(50);
        }
    }

    public void CauseDamage(float damage)
    {
        healthValue -= damage;
        healthBarValue.fillAmount = healthValue / 100f;
        if (healthValue <= 0)
        {
            Death();
        }
        StartCoroutine(MaterialChange());
    }

    IEnumerator MaterialChange()
    {
        rdr.material = damageMaterial;
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
