using UnityEngine;
using System.Collections.Generic;
namespace AetherRenaissance{
public class SelectionManager:MonoBehaviour{
public static SelectionManager Instance;
public List<GameObject> selectedUnits=new List<GameObject>();
void Awake(){Instance=this;}
public void SelectUnit(GameObject unit){
if(!selectedUnits.Contains(unit)){
selectedUnits.Add(unit);
Debug.Log("[SelectionManager] Unit selected: "+unit.name);
}
}
public void DeselectAll(){
selectedUnits.Clear();
Debug.Log("[SelectionManager] All units deselected");
}
}
}
