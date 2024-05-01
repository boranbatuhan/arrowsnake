using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float rotateLimit;
    private float halfWidth;
    float rotateSens=.1f;

    private bool isGamePlay = true;



    void Start()
    {
        halfWidth = Screen.width / 2;
    }


    void Update()
    {

        if(Input.GetMouseButton(0) && isGamePlay)
        {
            RotateHorizontal();
        }
    }

    private void RotateHorizontal()
    {
        float ypos = (Input.mousePosition.x - halfWidth) / halfWidth;
        float finalYpos = Mathf.Clamp(ypos * rotateLimit, -rotateLimit, rotateLimit);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, -finalYpos, 0)), rotateSens);

    }

    public void SetIsGamePlay(bool value)
    {
        isGamePlay = value;
    }
}
