using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] private GameObject meleeSounds;
    [SerializeField] private GameObject bluntSoundsDamage;

    public ObjectPool MeleeSoundsPool { get => meleeSoundsPool; }
    private ObjectPool meleeSoundsPool;

    public ObjectPool BluntSoundsDamagePool { get => bluntSoundsDamagePool; }
    private ObjectPool bluntSoundsDamagePool;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.name = "EffectsManager";

        meleeSoundsPool = new ObjectPool(10, meleeSounds, transform);
        bluntSoundsDamagePool = new ObjectPool(10, bluntSoundsDamage, transform);
    }

    public void PlayMeleeSound(Vector3 pos)
    {
        StartCoroutine(playEffect(meleeSoundsPool, 0.6f, pos));
    }
    public void PlayBluntSoundDamage(Vector3 pos)
    {
        StartCoroutine(playEffect(bluntSoundsDamagePool, 0.6f, pos));
    }


    private IEnumerator playEffect(ObjectPool pool, float timer, Vector3 pos)
    {
        GameObject g = pool.GetObject();
        g.transform.position = pos;
        g.SetActive(true);

        yield return new WaitForSeconds(timer);
        pool.ReturnObject(g);
    }
}
