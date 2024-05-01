using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControler : MonoBehaviour
{

    [SerializeField] TextMeshPro lifeText,lifeTextBack;
    [SerializeField] Material buffRed, buffGreen;
    [SerializeField] int life;

    PlayerShooting playerShooting;
    ParticleSystem popParticle;
    Transform buffArrow, buffSpeed;
    Animator animator;

    public static int arrowBuffCount;
    public static int speedBuffCount;
    int randomBuff = 2;

    

    private void Awake()
    {

        buffArrow = transform.GetChild(0);
        buffSpeed = transform.GetChild(1);
        popParticle = GetComponentInChildren<ParticleSystem>();
        playerShooting = FindAnyObjectByType<PlayerShooting>();
        animator = GetComponent<Animator>();    
        int randomLife = Random.Range(1, 6);
        life = randomLife*5;
    }
    private void Start()
    {

        // 0 = arrow up      
        // 1 = speed up
        // 2 = no buff
        randomBuff = Random.Range(0, 3);


        // arrow up
        if (randomBuff == 0 && arrowBuffCount<3)
        {
            buffArrow.gameObject.SetActive(true);
            buffSpeed.gameObject.SetActive(false);
            transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material = buffRed;
            arrowBuffCount++;

        }
        // speed up
        else if(randomBuff == 1 && speedBuffCount<3)
        {
            buffArrow.gameObject.SetActive(false);
            buffSpeed.gameObject.SetActive(true);
            transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material = buffGreen;

            speedBuffCount++;
        }
        // no buff
        else 
        {
            buffArrow.gameObject.SetActive(false);
            buffSpeed.gameObject.SetActive(false);
            randomBuff = 2;
        }


        lifeText.text = life + "";
        lifeTextBack.text = life + "";
    }



    public void DecreaseLife(int damage)
    {
        life -= damage;
            animator.ResetTrigger("hit");
            animator.SetTrigger("hit");
     
        if(life > 0)
        {
            lifeText.text = life + "";
            lifeTextBack.text = life + "";
        }

        if(life <=0)
        {
            DefeatedBody();

            if(randomBuff ==0)
            {
                ArrowUp();
            }
            else if(randomBuff==1)
            {
                SpeedUp();
            }
            else
            {
                return;
            }
        }

    }

    void DefeatedBody()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        transform.DOScale(Vector3.zero, .1f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            popParticle.Play();
            gameObject.SetActive(false);
        });
    }

    public void ArrowUp()
    {
        playerShooting.ArrowUp();

    }
    public void SpeedUp()
    {
        playerShooting.SpeedUp();
    }

    public int GetLife()
    {
        return life;
    }
    public void ResetBuffCount()
    {
        arrowBuffCount = 0;
        speedBuffCount = 0;
    }


}
