using UnityEngine;
using System;

public class GameObject : MonoBehaviour
{
    [SerializeField] private string objectName;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float solarCost = 50f;
    [SerializeField] private float quartzCost = 25f;
    
    private int currentHealth;
    private bool isDestroyed = false;
    
    public event Action<GameObject> OnObjectDestroyed;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        if (isDestroyed) return;
        
        currentHealth -= damage;
        Debug.Log($"[GameObject] {objectName} получил {damage} урона. Осталось: {currentHealth}/{maxHealth}");
        
        if (currentHealth <= 0)
        {
            DestroyObject();
        }
    }
    
    public void Repair(int amount)
    {
        if (isDestroyed) return;
        
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"[GameObject] {objectName} восстановлен на {amount}. Текущее здоровье: {currentHealth}/{maxHealth}");
    }
    
    private void DestroyObject()
    {
        isDestroyed = true;
        Debug.Log($"[GameObject] {objectName} уничтожен!");
        
        OnObjectDestroyed?.Invoke(this);
        
        // Visual effects
        PlayDestroyEffect();
        
        // Cleanup
        Destroy(gameObject, 0.5f);
    }
    
    private void PlayDestroyEffect()
    {
        // Add particle effects, sounds, etc.
        Debug.Log($"[GameObject] Эффект разрушения для {objectName}");
    }
    
    public bool IsAlive() => !isDestroyed && currentHealth > 0;
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    public string GetObjectName() => objectName;
    public float GetSolarCost() => solarCost;
    public float GetQuartzCost() => quartzCost;
}
