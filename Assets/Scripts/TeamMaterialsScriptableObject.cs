using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Team Materials", menuName = "ScriptableObjects/Team Material Data Object", order = 1)]
public class TeamMaterialsScriptableObject : ScriptableObject
{
    public Material Unassigned;
    public Material TeamA;
    public Material TeamB;
    public Material TeamC;
}
