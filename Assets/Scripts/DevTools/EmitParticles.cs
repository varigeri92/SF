using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitParticles : MonoBehaviour {

    private ParticleSystem _particleSystem;
    private bool _particlesPlayed = false;


	// Use this for initialization
	void Start () {

        try
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
        catch (System.Exception)
        {
            Debug.LogError(string.Format("{0} - No Object of Type ParticleSystem found on GameObject: {1}", System.DateTime.Now.ToShortTimeString(), gameObject.name));
            throw;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.Space))
        {
            PlayParticles();
        }
		
	}

    private void PlayParticles()
    {
        if (_particlesPlayed == false)
        {
            _particleSystem.Play();
            _particlesPlayed = true;
        }
        else
        {
            _particleSystem.Clear();
            _particlesPlayed = false;
            PlayParticles();
        }
    }
}
