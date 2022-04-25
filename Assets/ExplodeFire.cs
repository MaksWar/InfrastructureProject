using System;
using UnityEngine;

public class ExplodeFire : MonoBehaviour
{
	[SerializeField] private ParticleSystem explodeParticle;

	private void OnTriggerEnter(Collider other)
	{
		if(explodeParticle.isPlaying == false)
			explodeParticle.Play();
	}

	private void OnTriggerExit(Collider other)
	{
		if(explodeParticle.isPlaying)
			explodeParticle.Stop();
	}
}
