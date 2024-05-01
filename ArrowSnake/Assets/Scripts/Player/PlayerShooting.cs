using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] int damage=1;

    [SerializeField] float rayDistance;
    [SerializeField] float fireRate;
    [SerializeField] float nextFire;
 
    [SerializeField] bool isShooting;
    [SerializeField] bool isGameStart=false;
    
    
    [SerializeField] LayerMask hitLayer;
    [SerializeField] GameObject line;
    [SerializeField] GameObject aim;
    [SerializeField] GameObject arrowTemp;

    [SerializeField] Animator animator;
    [SerializeField] int animatorSpeed=1;



    EnemyControler enemyControler;
    float coolDown = 2f;
    private void Start()
    {
        line.GetComponent<LineRenderer>().SetPosition(1, Vector3.forward * rayDistance);
    }
    void Update()
    {
        animator.SetBool("shoot", isShooting);

        if (Input.GetMouseButton(0))
        {
            if (Time.time > coolDown)
            {
                isShooting = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = true;
            coolDown = Time.time+2;
        }

        if (isShooting && isGameStart)
        {
            Shooting();
        }

    }

    private void Shooting()
    {

        if (Time.time>nextFire)
        {


            nextFire= Time.time+fireRate;
            RaycastHit hit;
            Ray ray = new Ray(aim.transform.position, aim.transform.forward);
            Vector3 endPos = ray.GetPoint(rayDistance);
            // shooting
            if (Physics.Raycast(ray, out hit, rayDistance, hitLayer))
            {
                enemyControler = hit.collider.gameObject.GetComponent<EnemyControler>();
                enemyControler.DecreaseLife(damage);
                endPos = hit.point;
            }
            //shoot anim
            for (int i = 0; i < damage; i++)
            {
                GameObject arrow = Instantiate(arrowTemp, aim.transform.position + Vector3.right / 10 * i, Quaternion.identity);
                arrow.transform.DOMove(endPos,fireRate).OnComplete(() =>
                {
                    arrow.gameObject.SetActive(false);
                    Destroy(arrow);
                });
            }
        }

    }


    public void ArrowUp()
    {
        damage *= 2;
    }
    public void SpeedUp() 
    {
        float newSpeed = fireRate / 2;
        if(newSpeed <= 0.01f)
        {
            newSpeed = 0.01f;
        }
        fireRate = newSpeed;
        animatorSpeed += 1;
        animator.speed=animatorSpeed;
    }


    // draw ray in inspector

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Vector3 direction = aim.transform.TransformDirection(Vector3.forward) * rayDistance;
        Gizmos.DrawRay(aim.transform.position, direction);
    }
}
