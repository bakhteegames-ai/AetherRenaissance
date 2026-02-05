using UnityEngine;
namespace AetherRenaissance{
public class Building_Barracks:MonoBehaviour{
public int hp=500,cost=200;
public float buildTime=5f;
void Start(){Debug.Log("[Building_Barracks] Barracks constructed!");}
public void TrainUnit(string unitType){
if(ResourceSystem.Instance==null)return;
int unitCost=unitType=="Servo"?50:unitType=="Volt"?100:150;
if(ResourceSystem.Instance.SpendSolaris(unitCost)){
Debug.Log("[Building_Barracks] Training "+unitType+"...");
}
}
}
}
