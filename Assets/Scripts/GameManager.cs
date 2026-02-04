Assets/Scripts/GameManager.cs
using UnityEngine;
using System.Collections.Generic;

namespace AetherRenaissance
{
    /// <summary>
    /// Main Game Manager для Aether Renaissance RTS
    /// Основан на TDD v1.5.1 - Aetherpunk Clockwork Fantasy MVP
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // Singleton pattern
        public static GameManager Instance { get; private set; }

        [Header("Game Settings")]
        [Tooltip("Макро-ориентированная RTS с фокусом на решения, а не реакции")]
        public float gameSpeed = 1.0f;

        [Header("Resource System - TDD 3.1")]
        [Tooltip("Solaris - главный ресурс (аналог Gold)")]
        public float solarisAmount = 500f;
        
        [Tooltip("Quartz - tech ресурс для T2/T3 и Overcharge")]
        public float quartzAmount = 0f;

        [Header("Overcharge System - TDD 3.2")]
        [Tooltip("Множитель скорости постройки при активном Overcharge")]
        public float overchargeMultiplier = 1.5f;
        
        [Tooltip("Cooldown между использованиями Overcharge")]
        public float overchargeCooldown = 60f;
        
        private float overchargeCooldownTimer = 0f;
        private bool isOverchargeActive = false;

        [Header("Power Grid - TDD 4.0")]
        [Tooltip("Радиус покрытия Power Grid")]
        public float powerGridRadius = 30f;

        [Header("Combat Settings - TDD 5.0")]
        [Tooltip("Включить Friendly Fire для Siege урона")]
        public bool enableFriendlyFire = true;
        
        [Tooltip("Множитель урона Rock-Paper-Scissors")]
        public float damageTypeMultiplier = 1.5f;

        [Header("Anti-Stall - TDD 10.0")]
        [Tooltip("Время до Sudden Death если нет зданий")]
        public float suddenDeathGracePeriod = 30f;
        
        private float suddenDeathTimer = 0f;
        private bool isSuddenDeathActive = false;

        [Header("Debug")]
        public bool showDebugInfo = true;

        // Game state
        private List<GameObject> playerUnits = new List<GameObject>();
        private List<GameObject> playerBuildings = new List<GameObject>();

        void Awake()
        {
            // Singleton setup
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            InitializeGame();
        }

        void InitializeGame()
        {
            Debug.Log("[Aether Renaissance] Инициализация игры");
            Debug.Log("[TDD v1.5.1] Aetherpunk Clockwork Fantasy MVP");
            
            // Начальные ресурсы
            solarisAmount = 500f;
            quartzAmount = 0f;
        }

        void Update()
        {
            // Обновление таймеров
            if (overchargeCooldownTimer > 0)
            {
                overchargeCooldownTimer -= Time.deltaTime;
            }

            // Anti-Stall механика (TDD 10.0)
            CheckSuddenDeath();
        }

        #region Resource Management (TDD 3.0)
        
        public bool SpendResources(float solaris, float quartz)
        {
            if (solarisAmount >= solaris && quartzAmount >= quartz)
            {
                solarisAmount -= solaris;
                quartzAmount -= quartz;
                return true;
            }
            return false;
        }

        public void AddResources(float solaris, float quartz)
        {
            solarisAmount += solaris;
            quartzAmount += quartz;
        }

        #endregion

        #region Overcharge System (TDD 3.2)
        
        public bool ActivateOvercharge(float quartzCost)
        {
            if (overchargeCooldownTimer > 0)
            {
                Debug.Log($"[Overcharge] На перезарядке: {overchargeCooldownTimer:F1}с");
                return false;
            }

            if (quartzAmount < quartzCost)
            {
                Debug.Log("[Overcharge] Недостаточно Quartz!");
                return false;
            }

            quartzAmount -= quartzCost;
            isOverchargeActive = true;
            overchargeCooldownTimer = overchargeCooldown;
            
            Debug.Log($"[Overcharge] Активирован! Множитель: {overchargeMultiplier}x");
            return true;
        }

        public float GetOverchargeMultiplier()
        {
            return isOverchargeActive ? overchargeMultiplier : 1.0f;
        }

        #endregion

        #region Anti-Stall Sudden Death (TDD 10.0)
        
        void CheckSuddenDeath()
        {
            // Проверка: есть ли у игрока здания или активное строительство
            bool hasBuildings = playerBuildings.Count > 0;
            bool hasActiveConstruction = CheckActiveConstruction();

            if (!hasBuildings && !hasActiveConstruction)
            {
                if (!isSuddenDeathActive)
                {
                    isSuddenDeathActive = true;
                    suddenDeathTimer = 0f;
                    Debug.LogWarning("[Anti-Stall] Sudden Death активирован!");
                }

                suddenDeathTimer += Time.deltaTime;

                if (suddenDeathTimer >= suddenDeathGracePeriod)
                {
                    Debug.LogError("[Anti-Stall] GAME OVER - Sudden Death!");
                    // Здесь вызов GameOver
                }
            }
            else
            {
                if (isSuddenDeathActive)
                {
                    isSuddenDeathActive = false;
                    Debug.Log("[Anti-Stall] Sudden Death отменён");
                }
                suddenDeathTimer = 0f;
            }
        }

        bool CheckActiveConstruction()
        {
            // TODO: Проверка активного строительства
            // Должна проверять есть ли foundation или worker assigned
            return false;
        }

        #endregion

        #region Unit/Building Registration
        
        public void RegisterUnit(GameObject unit)
        {
            if (!playerUnits.Contains(unit))
            {
                playerUnits.Add(unit);
            }
        }

        public void UnregisterUnit(GameObject unit)
        {
            playerUnits.Remove(unit);
        }

        public void RegisterBuilding(GameObject building)
        {
            if (!playerBuildings.Contains(building))
            {
                playerBuildings.Add(building);
                Debug.Log($"[GameManager] Зарегистрировано здание: {building.name}");
            }
        }

        public void UnregisterBuilding(GameObject building)
        {
            playerBuildings.Remove(building);
            Debug.Log($"[GameManager] Здание уничтожено: {building.name}");
        }

        #endregion

        void OnGUI()
        {
            if (!showDebugInfo) return;

            GUILayout.BeginArea(new Rect(10, 10, 300, 200));
            GUILayout.Label("=== AETHER RENAISSANCE ===");
            GUILayout.Label($"Solaris: {solarisAmount:F0}");
            GUILayout.Label($"Quartz: {quartzAmount:F0}");
            GUILayout.Label($"Units: {playerUnits.Count}");
            GUILayout.Label($"Buildings: {playerBuildings.Count}");
            
            if (overchargeCooldownTimer > 0)
            {
                GUILayout.Label($"Overcharge CD: {overchargeCooldownTimer:F1}s");
            }
            
            if (isSuddenDeathActive)
            {
                GUILayout.Label($"SUDDEN DEATH: {suddenDeathTimer:F1}/{suddenDeathGracePeriod}s");
            }
            
            GUILayout.EndArea();
        }
    }
}
