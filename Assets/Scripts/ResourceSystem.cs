using UnityEngine;
using System;

public class ResourceSystem : MonoBehaviour
{
    [SerializeField] private float solarisPerSecond = 5f;
    [SerializeField] private float quartzPerSecond = 2f;
    [SerializeField] private float baseOverchargeCooldown = 60f;
    
    private float currentSolaris = 0f;
    private float currentQuartz = 0f;
    private float overchargeCooldownTimer = 0f;
    
    public event Action<float, float> OnResourcesChanged;
    public event Action<float> OnOverchargeCooldownChanged;
    
    void Update()
    {
        // Passive resource generation
        currentSolaris += solarisPerSecond * Time.deltaTime;
        currentQuartz += quartzPerSecond * Time.deltaTime;
        
        // Update cooldown
        if (overchargeCooldownTimer > 0)
        {
            overchargeCooldownTimer -= Time.deltaTime;
            OnOverchargeCooldownChanged?.Invoke(overchargeCooldownTimer);
        }
        
        OnResourcesChanged?.Invoke(currentSolaris, currentQuartz);
    }
    
    public void AddResources(float solaris, float quartz)
    {
        currentSolaris += solaris;
        currentQuartz += quartz;
        Debug.Log($"[ResourceSystem] Ресурсы добавлены: Solaris +{solaris}, Quartz +{quartz}");
    }
    
    public bool TrySpendResources(float solarisAmount, float quartzAmount)
    {
        if (currentSolaris >= solarisAmount && currentQuartz >= quartzAmount)
        {
            currentSolaris -= solarisAmount;
            currentQuartz -= quartzAmount;
            Debug.Log($"[ResourceSystem] Ресурсы потрачены: Solaris -{solarisAmount}, Quartz -{quartzAmount}");
            return true;
        }
        return false;
    }
    
    public void ActivateOvercharge()
    {
        if (overchargeCooldownTimer <= 0)
        {
            // Apply overcharge effects
            solarisPerSecond *= 2f;
            quartzPerSecond *= 2f;
            
            // Start cooldown
            overchargeCooldownTimer = baseOverchargeCooldown;
            
            Debug.Log("[ResourceSystem] Overcharge активирован!");
            
            // Reset after 10 seconds
            Invoke(nameof(DeactivateOvercharge), 10f);
        }
    }
    
    private void DeactivateOvercharge()
    {
        solarisPerSecond /= 2f;
        quartzPerSecond /= 2f;
        Debug.Log("[ResourceSystem] Overcharge завершен");
    }
    
    public float GetSolaris() => currentSolaris;
    public float GetQuartz() => currentQuartz;
}
