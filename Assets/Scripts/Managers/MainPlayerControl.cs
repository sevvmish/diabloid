using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class MainPlayerControl : MonoBehaviour
{
    [Inject] private InputControl inputControl;

    public Transform mainPlayerTransform {  get; private set; }
    public PlayerControl mainPlayerControl { get; private set; }

    private float timer;
    private Vector3 destination;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject g = Instantiate(Resources.Load<GameObject>("PlayerControl"), transform);
        g.transform.localPosition = Vector3.zero;
        g.transform.localEulerAngles = Vector3.zero;
        g.transform.localScale = Vector3.one;
        g.SetActive(true);

        mainPlayerTransform = g.transform;
        mainPlayerControl = g.GetComponent<PlayerControl>();
        mainPlayerControl.SetData();
    }

    private void Update()
    {
        

        if (timer > 0.05f)
        {
            timer = 0;
            
            if (inputControl.Direction != Vector2.zero)
            {
                destination = mainPlayerTransform.position + new Vector3(inputControl.Direction.x, 0, inputControl.Direction.y);
                mainPlayerControl.MoveToPoint(destination);
            }
            else if (inputControl.ClickPoint.magnitude > 0)
            {
                Vector2 dir = (new Vector2(inputControl.ClickPoint.x, inputControl.ClickPoint.z) - new Vector2(mainPlayerTransform.position.x, mainPlayerTransform.position.z)).normalized;
                destination = mainPlayerTransform.position + new Vector3(dir.x, 0, dir.y);
                mainPlayerControl.MoveToPoint(destination);
            }

        }
        else
        {
            timer += Time.deltaTime;
        }
    }

}
