using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CharacterAimer : MonoBehaviour
{
    public List<PlayerControl> Aims { get; private set; }

    private SphereCollider _collider;
    private int team;
    private bool isInited;
    private float radius;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.3f)
        {
            if (Aims.Count > 0) Aims.Clear();
        }
    }

    public void SetData(float aggroRadius, int team)
    {
        Aims = new List<PlayerControl>();
        Aims.Clear();
        radius = aggroRadius;

        if (_collider == null) _collider = GetComponent<SphereCollider>();
        _collider.center = Vector3.zero;
        _collider.radius = aggroRadius;
        _collider.isTrigger = true;

        
                
        this.team = team;
        isInited = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInited && other.gameObject.layer == 3 && other.gameObject.TryGetComponent(out PlayerControl c) && c.TeamID != team && c.IsAlive)
        {            
            addAim(c);            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _timer = 0;

        if (isInited && other.gameObject.layer == 3 && other.gameObject.TryGetComponent(out PlayerControl c) && c.TeamID != team && c.IsAlive)
        {            
            addAim(c);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isInited && other.gameObject.layer == 3 && other.gameObject.TryGetComponent(out PlayerControl c) && c.TeamID != team)
        {            
            removeAim(c);
        }
    }

    public void addAim(PlayerControl newAim)
    {
        if (Aims.Contains(newAim)) return;

        //aimCheck.Add(newAim);
        Aims.Add(newAim);
    }

    public void removeAim(PlayerControl newAim)
    {
        if (!Aims.Contains(newAim)) return;

        //aimCheck.Remove(newAim);
        Aims.Remove(newAim);
    }
}
