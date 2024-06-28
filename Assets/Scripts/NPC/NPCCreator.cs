using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCreator : MonoBehaviour
{
    [SerializeField] private CharacterTypes characterType = CharacterTypes.Skeleton;

    private PlayerControl mainPlayerControl;

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

        mainPlayerControl = g.GetComponent<PlayerControl>();
        mainPlayerControl.SetData(characterType, Globals.ENEMIES_TEAM);
    }


    private void Update()
    {
        if (timer > 0.05f)
        {
            timer = 0;
                        
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
