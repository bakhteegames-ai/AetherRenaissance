using UnityEngine;

namespace AetherRenaissance{
public class ResourceSystem:MonoBehaviour{
public static ResourceSystem Instance;
int solaris=500;
void Awake(){if(!Instance)Instance=this;else Destroy(gameObject);}
public int GetSolaris()=>solaris;
public void SetSolaris(int v){solaris=v;Debug.Log("Solaris:"+solaris);}
public void AddSolaris(int v){solaris+=v;Debug.Log("+"+v+" Solaris="+solaris);}
public bool SpendSolaris(int v){if(solaris>=v){solaris-=v;Debug.Log("-"+v);return true;}return false;}
public bool CanAfford(int v)=>solaris>=v;
}
}
