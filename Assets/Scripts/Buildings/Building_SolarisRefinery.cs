using UnityEngine;
namespace AetherRenaissance{
public class Building_SolarisRefinery:MonoBehaviour{
public int hp=300,cost=400;
public float buildTime=12f;
public float refineryRate=1.5f;
void Start(){Debug.Log("[Building_SolarisRefinery] Refinery constructed!");}
public void TrainUnit(string unitType){
if(ResourceSystem.Instance==null)return;
int unitCost=unitType=="Servo"?300:unitType=="Volt"?450:600;
if(ResourceSystem.Instance.SpendSolaris(unitCost)){
Debug.Log("[Building_SolarisRefinery] Refining "+unitType+"...");
}
}
}
}
