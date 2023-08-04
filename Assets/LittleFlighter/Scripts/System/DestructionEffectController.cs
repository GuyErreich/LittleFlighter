using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionEffectController : MonoBehaviour
{
    [SerializeField] List<GameObject> destructionEffects;

    private void OnDisable()
    {
        var index = Random.Range(0, destructionEffects.Count);

        Instantiate(destructionEffects[index], this.transform.position, Quaternion.identity, this.transform.parent);
    }
}
