using UnityEngine;
namespace AetherRenaissance{
public class CameraController:MonoBehaviour{
public float speed=20f,edge=10f;
void Update(){
Vector3 p=transform.position;
if(Input.GetKey(KeyCode.W)||Input.mousePosition.y>=Screen.height-edge)p.z+=speed*Time.deltaTime;
if(Input.GetKey(KeyCode.S)||Input.mousePosition.y<=edge)p.z-=speed*Time.deltaTime;
if(Input.GetKey(KeyCode.D)||Input.mousePosition.x>=Screen.width-edge)p.x+=speed*Time.deltaTime;
if(Input.GetKey(KeyCode.A)||Input.mousePosition.x<=edge)p.x-=speed*Time.deltaTime;
transform.position=p;
}
}
}
