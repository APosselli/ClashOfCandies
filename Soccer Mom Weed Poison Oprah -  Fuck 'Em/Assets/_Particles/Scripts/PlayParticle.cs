using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PlayParticle : MonoBehaviour
{
    private ParticleSystem mParticle;
    // Start is called before the first frame update
    void Start()
    {
        mParticle = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            mParticle.Play(true);
        }
    }
}
