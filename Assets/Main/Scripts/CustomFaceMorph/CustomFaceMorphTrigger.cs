using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFaceMorphTrigger : MonoBehaviour
{
    [Header("Hor/Ver Face Morph Slider Indexes")]
    public int m_BlendShapeIndex;
    //public int VerMorphIndex;

    public int m_MovementDirection;

    public bool m_HorOrVer;
    //public int verdir;
    //public int hordir;

    [Header("Movement Constraints")]
    public float XMin;
    public float XMax;
    public float YMin;
    public float YMax;

    [Header("Movement Scaler")]
    public float m_MovementScaler;


    [Header("Mirror Point")]
    public GameObject m_MirrorPoint; // if we move any side face blend point, then its mirror on the other side of the face also moves relatively

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = CustomFaceMorph.Instance.m_ColorTwo;
        CustomFaceMorph.Instance.SelectedFaceMorphTrigger(this.gameObject,m_MirrorPoint, XMin, XMax, YMin, YMax,m_BlendShapeIndex, m_MovementDirection, m_MovementScaler,m_HorOrVer);
    }
}
