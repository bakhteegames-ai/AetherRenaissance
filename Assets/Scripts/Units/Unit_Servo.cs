using UnityEngine;
namespace AetherRenaissance{
public class Unit_Servo:MonoBehaviour{
public int hp=50,cost=50;
public float speed=5f;
void Start(){
Debug.Log("[Unit_Servo] Worker spawned!");
}
public void GatherSolaris(){
if(ResourceSystem.Instance!=null){
ResourceSystem.Instance.AddSolaris(10);
Debug.Log("[Unit_Servo] +10 Solaris gathered");
}
}
}
}
