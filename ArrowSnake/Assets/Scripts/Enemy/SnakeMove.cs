using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    [SerializeField] Transform pathParent;
    [SerializeField] int gap;

    [SerializeField] GameObject headObj, bodyObj, tailObj;

    [SerializeField] float moveSpeed;
    [SerializeField] int bodyLength;


    List<GameObject> bodyParts = new();
    List<Vector3> posHistory = new();
    List<Quaternion> rotHistory= new();

    
    void Start()
    {
        for (int i = 0; i <= bodyLength; i++)
        {

            if (i == 0)
            {
                bodyParts.Add(Instantiate(headObj,transform.position,Quaternion.identity));
            }
            else if (i == bodyLength)
            {
                bodyParts.Add(Instantiate(tailObj, transform.position, Quaternion.identity));

            }
            else
            {
                bodyParts.Add(Instantiate(bodyObj, transform.position, Quaternion.identity));
            }
        }
        Invoke(nameof(ResetBuffCounts), 1f);


        // setloops-1  - - - - pathParent.childCount +1  - - - -- - - pathArray.Length +1

        Vector3[] pathArray = new Vector3[pathParent.childCount];

        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = pathParent.GetChild(i).position;
        }
        transform.DOPath(pathArray, moveSpeed, PathType.CatmullRom).SetLookAt(0.0001f);

    }

    private void Update()
    {
        posHistory.Insert(0,transform.position);
        rotHistory.Insert(0,transform.rotation);

        //int index = 0;
        for(int i =0; i<bodyParts.Count; i++)
        {
            //remove bodypart.
            if (i > 0 && i < bodyParts.Count - 1)
            {
                int bodylife = bodyParts[i].GetComponent<EnemyControler>().GetLife();
                if (bodylife <= 0)
                {
                    bodyParts.RemoveAt(i);
                }
            }


            Vector3 point = posHistory[Mathf.Min(i * gap, posHistory.Count - 1)];
            Quaternion rotation = rotHistory[Mathf.Min(i * gap, rotHistory.Count - 1)];
            Vector3 moveDirection = point - bodyParts[i].transform.position;
            bodyParts[i].transform.position += moveDirection *1 * Time.deltaTime;
            bodyParts[i].transform.rotation = rotation;

        }
    }

    public int GetBodyCount()
    {
        return bodyParts.Count-2;
    }
    private void ResetBuffCounts()
    {
        //reset buff counts
        EnemyControler enemyControler = FindAnyObjectByType<EnemyControler>();
        enemyControler.ResetBuffCount();
    }
}
