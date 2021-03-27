using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Water : MonoBehaviour
{
    public float power = 3.0f;
    public float scale = 1.0f;

    private Vector2 offcet = new Vector2(0f, 0f);
    public Vector2 offcetSpeed = new Vector2(0f, 0f);

    public ParticleSystem particleSystem;
    public ParticleSystem.Particle[] particles;

    void Start()
    {
        offcet = transform.position;
        particleSystem.Emit(10000);
        particles = new ParticleSystem.Particle[10000];
    }

    void LateUpdate()
    {
        offcet += offcetSpeed * Time.deltaTime;
        particleSystem.GetParticles(particles);
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                float xCoord = offcet.x + i * scale;
                float yCoord = offcet.y + j * scale;
                particles[j * 100 + i].position = transform.position + new Vector3(i, (Mathf.PerlinNoise(xCoord, yCoord)) * power, j)/5;
            }
        }
        particleSystem.SetParticles(particles);
    }
}

//Создание портов