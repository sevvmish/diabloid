using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour, IPlayer
{    
    public float CurrentSpeed { get; private set; }
    public int TeamID {get; private set;}
    public Character Character { get; private set; }
    public Transform Transform { get; private set; }

    private NavMeshAgent agent;
    private CharacterAimer aimer;
    private AnimationControl animationControl;
    private CapsuleCollider capsuleCollider;
    private HitPerformer hitPerformer;

    private float hitTimer;
    private IPlayer aim;
    private EffectsManager effects;
    private bool isMadeDead;

    private void Start()
    {
        effects = GameObject.Find("EffectsManager").GetComponent<EffectsManager>();
    }

    public void SetData(CharacterTypes charType, int team)
    {
        Transform = transform;

        agent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        Character = new Character(charType);
        TeamID = team;

        agent.enabled = true;
        agent.speed = Character.MaxSpeed;

        capsuleCollider.enabled = true;
        capsuleCollider.radius = Character.BodyRadius;

        GameObject g = Instantiate(Character.Skin, transform);
        g.transform.localPosition = Vector3.zero;
        g.transform.localEulerAngles = Vector3.zero;
        g.transform.localScale = Vector3.one;
        g.SetActive(true);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out CharacterAimer charAimer))
            {
                aimer = charAimer;
                aimer.SetData(3, TeamID);
            }
            else if (transform.GetChild(i).TryGetComponent(out Animator anim))
            {
                anim.gameObject.AddComponent<AnimationControl>();
                animationControl = anim.GetComponent<AnimationControl>();
                animationControl.SetData(this);
            }
        }

        hitPerformer = GetComponent<HitPerformer>();
        hitPerformer.SetData(animationControl);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Character.IsAlive && !isMadeDead)
        {            
            isMadeDead = true;
            agent.ResetPath();
            agent.enabled = false;
            capsuleCollider.enabled = false;
            return;
        }
        else if (!Character.IsAlive && isMadeDead)
        {            
            return;
        }

        CurrentSpeed = agent.velocity.magnitude;

        
        if (hitTimer > 0.05f)
        {
            hitTimer = 0;
            if (aim != null && !agent.hasPath)
            {                
                if (!aim.Character.IsAlive || TeamID == aim.TeamID)
                {
                    aim = null;
                }
                else
                {
                    TryHitOrMove(aim);
                }
            }
            else if (aim != null && agent.hasPath)
            {
                TryHitOrMove(aim);
            }
            else if (aim == null && agent.hasPath)
            {
                //
            }
            else
            {
                idleBehaviour();
            }
        }
        else
        {
            hitTimer += Time.deltaTime;
        }
    }

    private void idleBehaviour()
    {
        if (CurrentSpeed == 0 && aimer.Aims.Count > 0)
        {
            if (aimer.Aims.Count == 1 && aimer.Aims[0].Character.IsAlive)
            {
                TryHitOrMove(aimer.Aims[0]);
            }
            else
            {
                float distance = float.MaxValue;
                IPlayer player = null;

                for (int i = 0; i < aimer.Aims.Count; i++)
                {
                    if (!aimer.Aims[i].Character.IsAlive)
                    {
                        aimer.removeAim(aimer.Aims[i]);
                        continue;
                    }

                    float currDist = (aimer.Aims[i].Transform.position - transform.position).magnitude;
                    if (currDist < distance)
                    {
                        distance = currDist;
                        player = aimer.Aims[i];
                    }
                }

                if (player != null)
                {
                    TryHitOrMove(player);
                }
            }
        }
    }

    public void MoveToPoint(Vector3 _point)
    {
        if (isMadeDead) return;

        aim = null;
        agent.SetDestination(_point);
        hitTimer = 0;
    }
    
    public void TryHitOrMove(IPlayer pc)
    {
        if (isMadeDead) return;

        aim = pc;
        hitTimer = 0;

        if ((aim.Transform.position - transform.position).magnitude <= (Character.HitRadius * 1.2f) && aim.Character.IsAlive)
        {
            transform.LookAt(aim.Transform.position);
            agent.ResetPath();
            hitPerformer.MeleeHit();
            aim = null;
        }
        else
        {
           
            agent.SetDestination(pc.Transform.position);
        }
    }

    public void ReceiveHit(IPlayer enemy)
    {
        effects.PlayBluntSoundDamage(transform.position + Vector3.up);        
        Character.ReceiveHit(enemy.Character.Damage);
    }

    
}
