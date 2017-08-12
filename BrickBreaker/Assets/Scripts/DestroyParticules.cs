using UnityEngine;
using System.Collections;

public class DestroyParticules : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, this.GetComponent<ParticleSystem>().duration);
    }
}