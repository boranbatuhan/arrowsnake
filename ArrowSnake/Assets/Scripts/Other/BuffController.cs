using UnityEngine;

public class BuffController : MonoBehaviour
{

    Vector3 campos;


    void Update()
    {
        campos = Camera.main.transform.position;
        //transform.localRotation = Quaternion.LookRotation(campos);
        transform.LookAt(campos, Vector3.up);
    }
}
