using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{    
    public Transform target;
    public float speed;
    public int damage;

    public void setTarget(Transform t){
        target = t;
    }
    public void setSpeed(float s){
        speed = s;
    }
    public void setDamage(int d){
        damage = d;
    }

    private void targetEnemy()
    {
        if (target == null){
            Destroy(gameObject);
            return;
        }
        
        transform.LookAt(target);

        if ((transform.position - target.position).magnitude > 0.5){
            transform.Translate(0.0f,0.0f, speed* Time.deltaTime);               
        }else{
            Enemy enemyTarget = target.GetComponent<Enemy>();
            if (enemyTarget != null){
                enemyTarget.takeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
    
    private void Update() {
        targetEnemy();
    }
}
