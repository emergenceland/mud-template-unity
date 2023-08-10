using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateType
{
    Normal,
    Happy,
    Sad
}

public class BallTest : MonoBehaviour
{
    public string key; 
    public StateType state;

    public Material[] materials;
    public MeshRenderer mr;
    public void UpdateState(StateType newState) {
        state = newState;
        mr.sharedMaterial = materials[(int)newState];
    }

}
