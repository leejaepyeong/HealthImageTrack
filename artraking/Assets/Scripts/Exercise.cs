using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise : MonoBehaviour
{
    public string exercise;
    public Animator anim;

    public bool isOn = false;
    public bool isExercise;

    private void Update()
    {
        if(!isExercise)
        {
            isExercise = true;
            anim.SetTrigger(exercise);
        }
        
    }
}
