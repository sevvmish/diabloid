using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HitPerformer : MonoBehaviour
{
    private NavMeshAgent agent;
    private AnimationControl animationControl;
    private PlayerControl playerControl;
    private Character character;
    
    private AssetManager assets;
    private EffectsManager effects;

    private bool isReady { get => timer >= character.AttackCooldown; }
    private float timer;

    private void Start()
    {
        assets = GameObject.Find("AssetManager").GetComponent<AssetManager>();
        effects = GameObject.Find("EffectsManager").GetComponent<EffectsManager>();
    }


    private void Update()
    {
        if (timer >= character.AttackCooldown)
        {
            //            
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public void SetData(AnimationControl _animator)
    {
        agent = GetComponent<NavMeshAgent>();
        animationControl = _animator;
        playerControl = GetComponent<PlayerControl>();
        character = playerControl.Character;
    }

    public void MeleeHit()
    {
        if (!isReady) return;
        StartCoroutine(playMeleeHit());
    }
    private IEnumerator playMeleeHit()
    {
        animationControl.Hit();
        effects.PlayMeleeSound(transform.position + Vector3.up);
        agent.ResetPath();
        timer = 0;
        yield return new WaitForSeconds(0.2f);

        GameObject g = assets.MeleeHitBoxPool.GetObject();
        g.transform.parent = transform;        
        g.transform.localScale = new Vector3(1, 1, character.HitRadius);
        g.transform.localEulerAngles = Vector3.zero;    
        g.transform.localPosition = Vector3.zero;
        g.transform.position += transform.forward * character.HitRadius / 2f  + Vector3.up;

        g.GetComponent<DamageDealer>().SetData(playerControl, 1);
        g.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        assets.MeleeHitBoxPool.ReturnObject(g);
    }
}
