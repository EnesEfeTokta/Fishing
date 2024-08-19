using System.Collections;
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
        if (healthValue <= 0)
        {
            Death();
        }
        StartCoroutine(MaterialChange());
    }

    IEnumerator MaterialChange()
    {
        rdr.material = damageMaterial;
        yield return new WaitForSeconds(0.1f);
        rdr.material = originalMaterial;
    }

    void Death()
    {
        Destroy(this.gameObject);
    }
}
