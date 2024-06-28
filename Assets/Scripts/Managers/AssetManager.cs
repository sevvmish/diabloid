using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    [SerializeField] private GameObject meleeHitBox;

    public ObjectPool MeleeHitBoxPool { get => meleeHitBoxPool; }
    private ObjectPool meleeHitBoxPool;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.name = "AssetManager";

        meleeHitBoxPool = new ObjectPool(10, meleeHitBox, transform);
    }

    
}
