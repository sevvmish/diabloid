using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class CameraControl : MonoBehaviour
{
    [Inject] private MainPlayerControl mainPlayerControl;
    [Inject] private Camera mainCamera;

    private Transform cameraBody;
    private Transform mainPlayer;

    private readonly Vector3 defaultPosition = new Vector3(0, 9, -5.7f);
    private readonly Vector3 defaultRotation = new Vector3(55, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        cameraBody = mainCamera.transform.parent;
        cameraBody.position = defaultPosition;
        cameraBody.eulerAngles = defaultRotation;
        mainPlayer = mainPlayerControl.mainPlayerTransform;

        if (Globals.IsMobile)
        {
            mainCamera.fieldOfView = 60;
        }
        else
        {
            mainCamera.fieldOfView = 65;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cameraBody.position = mainPlayer.position + defaultPosition;
    }
}
