using UnityEngine;

namespace AetherRenaissance
{
    /// <summary>
    /// Game Bootstrap - Auto-initializes game on Play button press
    /// Attach to empty GameObject in scene
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Starting Resources")]
        [SerializeField] private int startingSolaris = 500;
        
        [Header("Starting Units")]
        [SerializeField] private int startingServoCount = 3;
        [SerializeField] private GameObject servoPrefab;
        [SerializeField] private Vector3 spawnPosition = new Vector3(0, 1, 0);
        
        private void Start()
        {
            Debug.Log("[GameBootstrap] Initializing Aether Renaissance - Phase 0...");
            
            // Initialize Resource System
            InitializeResourceSystem();
            
            // Spawn starting units
            SpawnStartingUnits();
            
            // Initialize camera
            InitializeCamera();
            
            Debug.Log("[GameBootstrap] Game initialized! Ready to play.");
        }
        
        private void InitializeResourceSystem()
        {
            ResourceSystem resourceSystem = FindObjectOfType<ResourceSystem>();
            
            if (resourceSystem == null)
            {
                GameObject rsGo = new GameObject("ResourceSystem");
                resourceSystem = rsGo.AddComponent<ResourceSystem>();
            }
            
            resourceSystem.SetSolaris(startingSolaris);
            Debug.Log($"[GameBootstrap] Resource System initialized with {startingSolaris} Solaris");
        }
        
        private void SpawnStartingUnits()
        {
            if (servoPrefab == null)
            {
                Debug.LogWarning("[GameBootstrap] Servo prefab not assigned! Creating placeholder cubes.");
                
                // Create placeholder Servo units
                for (int i = 0; i < startingServoCount; i++)
                {
                    GameObject servo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    servo.name = $"Servo_{i}";
                    servo.transform.position = spawnPosition + new Vector3(i * 2f, 0, 0);
                    servo.AddComponent<Rigidbody>().useGravity = false;
                    
                    // Add Servo component if exists
                    if (servo.GetComponent<Unit_Servo>() == null)
                    {
                        servo.AddComponent<Unit_Servo>();
                    }
                }
            }
            else
            {
                for (int i = 0; i < startingServoCount; i++)
                {
                    Vector3 pos = spawnPosition + new Vector3(i * 2f, 0, 0);
                    Instantiate(servoPrefab, pos, Quaternion.identity);
                }
            }
            
            Debug.Log($"[GameBootstrap] Spawned {startingServoCount} Servo units");
        }
        
        private void InitializeCamera()
        {
            Camera mainCam = Camera.main;
            if (mainCam != null && mainCam.GetComponent<CameraController>() == null)
            {
                mainCam.gameObject.AddComponent<CameraController>();
                Debug.Log("[GameBootstrap] Camera controller added");
            }
        }
    }
}
