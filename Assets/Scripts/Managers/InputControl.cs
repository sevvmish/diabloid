using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

public class InputControl : MonoBehaviour
{
    [Inject] private Joystick joystick;
    [Inject] private Camera _camera;

    public Vector2 Direction { get; private set; }
    public Vector3 ClickPoint { get; private set; }
    public PlayerControl Aim { get; private set; }

    private LayerMask blockMask;
    private Ray ray;
    private RaycastHit hit;
    private Transform cameraTransform;

    private void Start()
    {
        if (!Globals.IsMobile)
        {
            joystick.gameObject.SetActive(false);
        }

        cameraTransform = _camera.transform.parent;
        blockMask = LayerMask.GetMask(new string[] { "Ground", "Player" }); 
    }

    // Update is called once per frame
    void Update()
    {
        Direction = Vector2.zero;
        ClickPoint = Vector3.zero;
        Aim = null;

        if (Globals.IsMobile)
        {
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                Direction = joystick.Direction;
            }
        }
        else
        {
            if (Input.anyKey)
            {
                float x = 0;
                float y = 0;

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    x = -1;
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    x = 1;
                }

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    y = 1;
                }
                else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    y = -1;
                }

                Direction = new Vector2(x, y);
            }
            
            if (Input.GetMouseButton(0))
            {
                ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100, blockMask, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.gameObject.TryGetComponent(out PlayerControl pc) && pc.Character.IsAlive && pc.TeamID == Globals.ENEMIES_TEAM)
                    {
                        Aim = pc;
                    }

                    ClickPoint = hit.point;
                }
            }
        }


    }
}
