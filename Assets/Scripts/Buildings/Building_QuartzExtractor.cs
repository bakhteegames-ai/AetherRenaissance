using UnityEngine;
namespace AetherRenaissance{
public class Building_QuartzExtractor:MonoBehaviour{
public int hp=400,cost=600;
public float buildTime=15f;
public float extractionRate=0.8f;
void Start(){Debug.Log("[Building_QuartzExtractor] Extractor operational!");}
public void ExtractQuartz(){
if(ResourceSystem.Instance==null)return;
ResourceSystem.Instance.AddQuartz((int)(extractionRate*10));
Debug.Log("[Building_QuartzExtractor] Extracting Quartz...");
}
}
}
