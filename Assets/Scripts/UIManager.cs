using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Resource Display")]
    [SerializeField] private TextMeshProUGUI solarisText;
    [SerializeField] private TextMeshProUGUI quartzText;
    [SerializeField] private TextMeshProUGUI unitsText;
    [SerializeField] private TextMeshProUGUI buildingsText;
    
    [Header("Overcharge")]
    [SerializeField] private Button overchargeButton;
    [SerializeField] private TextMeshProUGUI overchargeCooldownText;
    [SerializeField] private Image overchargeFillImage;
    
    [Header("Notifications")]
    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private TextMeshProUGUI notificationText;
    
    private ResourceSystem resourceSystem;
    
    void Start()
    {
        resourceSystem = FindObjectOfType<ResourceSystem>();
        
        if (resourceSystem != null)
        {
            resourceSystem.OnResourcesChanged += UpdateResourceDisplay;
            resourceSystem.OnOverchargeCooldownChanged += UpdateOverchargeCooldown;
        }
        
        if (overchargeButton != null)
        {
            overchargeButton.onClick.AddListener(OnOverchargeClicked);
        }
        
        HideNotification();
    }
    
    void Update()
    {
        if (GameManager.Instance != null)
        {
            UpdateCounts();
        }
    }
    
    private void UpdateResourceDisplay(float solaris, float quartz)
    {
        if (solarisText != null)
            solarisText.text = $"Solaris: {solaris:F0}";
            
        if (quartzText != null)
            quartzText.text = $"Quartz: {quartz:F0}";
    }
    
    private void UpdateCounts()
    {
        if (unitsText != null)
            unitsText.text = $"Units: {GameManager.Instance.playerUnits.Count}";
            
        if (buildingsText != null)
            buildingsText.text = $"Buildings: {GameManager.Instance.playerBuildings.Count}";
    }
    
    private void UpdateOverchargeCooldown(float cooldown)
    {
        if (overchargeCooldownText != null)
        {
            if (cooldown > 0)
            {
                overchargeCooldownText.text = $"CD: {cooldown:F0}s";
                overchargeButton.interactable = false;
            }
            else
            {
                overchargeCooldownText.text = "Ready!";
                overchargeButton.interactable = true;
            }
        }
        
        if (overchargeFillImage != null && resourceSystem != null)
        {
            // Update fill based on cooldown progress
            overchargeFillImage.fillAmount = 1f - (cooldown / 60f);
        }
    }
    
    private void OnOverchargeClicked()
    {
        if (resourceSystem != null)
        {
            resourceSystem.ActivateOvercharge();
            ShowNotification("Оверчардж активирован!");
        }
    }
    
    public void ShowNotification(string message)
    {
        if (notificationPanel != null && notificationText != null)
        {
            notificationPanel.SetActive(true);
            notificationText.text = message;
            
            // Auto-hide after 3 seconds
            Invoke(nameof(HideNotification), 3f);
        }
    }
    
    private void HideNotification()
    {
        if (notificationPanel != null)
        {
            notificationPanel.SetActive(false);
        }
    }
    
    void OnDestroy()
    {
        if (resourceSystem != null)
        {
            resourceSystem.OnResourcesChanged -= UpdateResourceDisplay;
            resourceSystem.OnOverchargeCooldownChanged -= UpdateOverchargeCooldown;
        }
    }
}
