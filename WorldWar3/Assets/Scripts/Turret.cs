using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;

    [Header("Unity Setup Fields")]
    public float range = 32f;
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;


    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 5f;
    private float fireCountdown = 1f;
    public Transform firePoint;
    public int damage;




    public Transform partToRotate;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    
    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            return;
        }
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot(target);
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

    }

    void Shoot(Transform target)
    {
        GameObject projectileGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //projectile projectile = projectileGO.GetComponent<projectile>();
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null){
            projectile.setTarget(target);
            projectile.setDamage(damage);
        }
    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
          //  targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
