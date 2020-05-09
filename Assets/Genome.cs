using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Genome
{
    public static int skillCount = 5;

    public float[] weights;
    public int[] skills;

    public Genome(int size)
    {
        weights = new float[size];
        skills = new int[skillCount];
        for(int i = 0; i < size; i++)
        {
            weights[i] = UnityEngine.Random.Range(-1f, 1f);
        }
    }

    public Genome(Genome a)
    {
        weights = new float[a.weights.Length];
        Array.Copy(a.weights, 0, weights, 0, a.weights.Length);
        skills = new int[skillCount];
        Array.Copy(a.skills, 0, skills, 0, skillCount);
    }

    public void Mutate(float value)
    {
        for(int i = 0; i < weights.Length; i++)
        {
            if(UnityEngine.Random.value < 0.1) weights[i] += UnityEngine.Random.Range(-value, value);
        }
        for(int i = 0; i < skillCount; i++)
        {
            if(UnityEngine.Random.value < 0.05) {
                skills[i] = UnityEngine.Random.Range(0, 4);
            }
        }
    }
    
}
