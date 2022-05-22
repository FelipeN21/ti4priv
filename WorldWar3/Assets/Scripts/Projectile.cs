using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{    
    public Transform target;
    public float speed;

    public void setTarget(Transform t){
        target = t;
    }
    public void setSpeed(float s){
        speed = s;
    }
    
    private void Update() {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        transform.LookAt(target);

        if ((transform.position - target.position).magnitude > 0.5)
        {
            transform.Translate(0.0f,0.0f, speed* Time.deltaTime);               
        }
    }
}
