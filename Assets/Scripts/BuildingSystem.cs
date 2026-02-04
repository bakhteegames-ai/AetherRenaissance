using UnityEngine;
using System.Collections.Generic;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private GameObject buildingPreviewPrefab;
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private float gridSize = 1f;
    
    private GameObject currentPreview;
    private bool isPlacementMode = false;
    private GameObject selectedBuildingPrefab;
    
    void Update()
    {
        if (isPlacementMode && currentPreview != null)
        {
            UpdateBuildingPreview();
            
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding();
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CancelPlacement();
            }
        }
    }
    
    public void StartBuildingPlacement(GameObject buildingPrefab)
    {
        selectedBuildingPrefab = buildingPrefab;
        isPlacementMode = true;
        
        currentPreview = Instantiate(buildingPrefab);
        // Make preview semi-transparent
        SetPreviewMaterial(currentPreview, true);
    }
    
    private void UpdateBuildingPreview()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainLayer))
        {
            Vector3 snappedPosition = SnapToGrid(hit.point);
            currentPreview.transform.position = snappedPosition;
            
            // Check if placement is valid
            bool isValid = CheckPlacementValidity(snappedPosition);
            SetPreviewColor(currentPreview, isValid);
        }
    }
    
    private Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;
        return new Vector3(x, position.y, z);
    }
    
    private bool CheckPlacementValidity(Vector3 position)
    {
        // Simple overlap check
        Collider[] colliders = Physics.OverlapSphere(position, gridSize / 2f);
        return colliders.Length == 0;
    }
    
    private void PlaceBuilding()
    {
        if (CheckPlacementValidity(currentPreview.transform.position))
        {
            GameObject building = Instantiate(selectedBuildingPrefab, 
                                             currentPreview.transform.position, 
                                             currentPreview.transform.rotation);
            
            GameManager.Instance.RegisterBuilding(building);
            Debug.Log($"[BuildingSystem] Здание размещено: {building.name}");
        }
    }
    
    private void CancelPlacement()
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }
        isPlacementMode = false;
    }
    
    private void SetPreviewMaterial(GameObject obj, bool isPreview)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (isPreview)
            {
                Material mat = renderer.material;
                Color color = mat.color;
                color.a = 0.5f;
                mat.color = color;
            }
        }
    }
    
    private void SetPreviewColor(GameObject obj, bool isValid)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        Color color = isValid ? Color.green : Color.red;
        color.a = 0.5f;
        
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }
}
