using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private Collider _collider;
    private int victimsAmount;
    private IPlayer player;
    private HashSet<IPlayer> players = new HashSet<IPlayer>();

    public void SetData(IPlayer p, int victimsAmount)
    {
        player = p;
        this.victimsAmount = victimsAmount;
    }


    private void OnEnable()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = true;
        players.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && other.TryGetComponent(out IPlayer p) && p.TeamID != player.TeamID && !players.Contains(p))
        {
            players.Add(p);
            
            p.ReceiveHit(player);

            if (players.Count >= victimsAmount)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
