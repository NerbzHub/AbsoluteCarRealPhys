﻿using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// Class:
//      Particle Generator: Constantly generates liquid particles.
//-------------------------------------------------------------------------------------

public class LiquidGenerator : MonoBehaviour
{

    public GameObject particle = null;

    public float particleLifetime = 3.0f;

    // Is there a initial force the particles should have?
    public Vector3 particleForce = Vector3.zero;

    // If no state is set the default particle is water. 
    public Liquid3DParticle.LiquidStates particlesState = Liquid3DParticle.LiquidStates.Water;

    // How much time until the next particle spawns
    private const float SPAWN_INTERVAL = 0.025f;

    // Container to keep all the particles in a single object within the unity heirarchy
    private Transform m_sceneParticleHolder;
    private float m_spawnTimer = 0.0f;

    //-------------------------------------------------------------------------------------
    // Awake runs when the gameObject is set to awake.
    //-------------------------------------------------------------------------------------
    void Awake()
    {
        // Reset spawn timer
        m_spawnTimer = 0.0f;
        m_sceneParticleHolder = new GameObject("ParticleHolder").transform;
    }

    //-------------------------------------------------------------------------------------
    // This occurrs every frame.
    //-------------------------------------------------------------------------------------
    void Update()
    {
        m_spawnTimer += Time.deltaTime;
        if (m_spawnTimer >= SPAWN_INTERVAL)
        {
            // Create a new particle.
            GameObject newLiquidParticle = Instantiate(particle) as GameObject; // Spawn a particle.

            newLiquidParticle.transform.position = transform.position;// Relocate to the spawner position.
            newLiquidParticle.GetComponent<Rigidbody>().AddForce(particleForce * 500);

            // Configure the new particle.
            Liquid3DParticle particleScript = newLiquidParticle.GetComponent<Liquid3DParticle>(); // Get the particle script.
            // particleScript.SetLifeTime(particleLifetime); // Set each particle lifetime.
            particleScript.SetState(particlesState); // Set the particle State.	

            // Keep the scene tidy and add the particle to the container transform.
            newLiquidParticle.transform.SetParent(m_sceneParticleHolder);

            // Reset spawn timer.
            m_spawnTimer = 0.0f;
        }
    }
}
