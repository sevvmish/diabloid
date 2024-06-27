using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{
    public bool IsAlive { get; private set; } = true;
    public float CurrentSpeed { get; private set; }
    public int TeamID {get; private set;}


    private NavMeshAgent agent;
    private CharacterAimer aimer;
    private AnimationControl animationControl;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetData()
    {
        GameObject g = Instantiate(Resources.Load<GameObject>("Skins/viking01"), transform);
        g.transform.localPosition = Vector3.zero;
        g.transform.localEulerAngles = Vector3.zero;
        g.transform.localScale = Vector3.one;
        g.SetActive(true);


        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out CharacterAimer charAimer))
            {
                aimer = charAimer;
                aimer.SetData(3, 1);
            }
            else if (transform.GetChild(i).TryGetComponent(out Animator anim))
            {
                anim.gameObject.AddComponent<AnimationControl>();
                animationControl = anim.GetComponent<AnimationControl>();
                animationControl.SetData(this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurrentSpeed = agent.velocity.magnitude;
    }

    public void MoveToPoint(Vector3 _point)
    {
        agent.SetDestination(_point);
    }
}
