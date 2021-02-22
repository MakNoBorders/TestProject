using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFaceMorphTrigger : MonoBehaviour
{
    [Header("Hor/Ver Face Morph Slider Indexes")]
    public int HorMorphIndex;
    public int VerMorphIndex;

    public int verdir;
    public int hordir;

    [Header("Movement Constraints")]
    public float XMin;
    public float XMax;
    public float YMin;
    public float YMax;

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = CustomFaceMorph.Instance.m_ColorTwo;
        CustomFaceMorph.Instance.SelectedFaceMorphTrigger(this.gameObject, XMin, XMax, YMin, YMax,HorMorphIndex,VerMorphIndex,hordir,verdir);
    }
}
