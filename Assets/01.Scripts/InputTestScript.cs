using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputTestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    SharkController controller;
    [Header("회전 관련 변수값")]
    public float rotSpeed = 1f;

    private float rot;


    Vector2 prevDir = Vector3.zero;
    float rotationTimer =1f;
    float timeGoal = 1f;
    void Start()
    {
        controller = new SharkController();
        //controller.Player.OnMove.performed += OnMove;
        controller.Player.Enable();
        rot = 1/rotSpeed;
    }
    private void FixedUpdate()
    {
        OnRotate();
    }
    private void OnRotate()
    {
        Vector2 dir = controller.Player.OnMove.ReadValue<Vector2>();
        if (dir.x == 0f && dir.y == 0f)
        {
            return;
        }
        else 
        {
            //Vector2 relPos = new Vector2(dir.x - transform.position.x, dir.x - transform.position.x);
            //float rad = Mathf.Atan2(relPos.y, relPos.x);
            float deg = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);

            float deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, deg);
            if (dir != prevDir)
            {
                prevDir = dir;
                rotationTimer = 0f;
                timeGoal = (Mathf.Abs(deltaAngle) / 180f)*rot;
            }
            else if (rotationTimer>= timeGoal)
            {
                return;
            }
            rotationTimer += Time.fixedDeltaTime;
            float timePercent = rotationTimer / timeGoal;
            transform.eulerAngles += new Vector3(0f, 0f, timePercent * deltaAngle);
        }
    }
    private void OnDestroy()
    {
        //controller.Player.OnMove.performed -= OnMove;
        controller.Player.Disable();
    }
}
