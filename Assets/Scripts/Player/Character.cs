using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public float BodyRadius { get; private set; }

    public float HitRadius { get; private set; }
    public float AttackCooldown { get; private set; }
    public float Damage {  get; private set; }

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public float MaxSpeed { get; private set; }
    public float CurrentSpeed { get; private set; }
    
    public CharacterTypes CharacterType { get; private set; }
    public GameObject Skin { get; private set; }
    public bool IsAlive { get => CurrentHealth > 0; }

    public Character(){}

    public Character(CharacterTypes _type)
    {
        switch(_type)
        {
            case CharacterTypes.Viking:
                Viking();
                break;

            case CharacterTypes.Skeleton:
                Skeleton();
                break;
        }
    }

    public void ReceiveHit(float damage)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= damage;

        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
    }

    private void Viking()
    {
        CharacterType = CharacterTypes.Viking;
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
        MaxSpeed = 5;
        CurrentSpeed = MaxSpeed;
        BodyRadius = 0.3f;
        HitRadius = 1.5f;
        AttackCooldown = 0.5f;
        Damage = 15f;
        Skin = Resources.Load<GameObject>("Skins/viking01");
    }

    private void Skeleton()
    {
        CharacterType = CharacterTypes.Skeleton;
        MaxHealth = 50;
        CurrentHealth = MaxHealth;
        MaxSpeed = 3;
        CurrentSpeed = MaxSpeed;
        BodyRadius = 0.6f;
        HitRadius = 1f;
        AttackCooldown = 0.5f;
        Damage = 5f;
        Skin = Resources.Load<GameObject>("Skins/Skeleton01");
    }
}

public enum CharacterTypes
{
    Viking,
    Skeleton
}
