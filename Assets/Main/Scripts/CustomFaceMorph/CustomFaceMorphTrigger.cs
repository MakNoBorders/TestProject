using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFaceMorphTrigger : MonoBehaviour
{
    [Header("Hor/Ver Face Morph Slider Indexes")]
    public int m_BlendShapeIndex;
    public int m_MovementDirection;
    public bool m_HorOrVer;  // Either the Trigger will move on Horizontal or Vertical

    [Header("Movement Constraints")] // Movement region of the trigger 
    public float XMin;
    public float XMax;
    public float YMin;
    public float YMax;

    [Header("Movement Scaler")]
    public float m_MovementScaler;

    [Header("Mirror Point")]
    public GameObject m_MirrorPoint; // In Zepeto if we move one point on face then, another point on the other side of the face will also move

    [Header("45 Degree Movement")]
    public bool m_45DegreeMovement;

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = CustomFaceMorph.Instance.m_ColorTwo;
        CustomFaceMorph.Instance.SelectedFaceMorphTrigger(this.gameObject,m_MirrorPoint, XMin, XMax, YMin, YMax,m_BlendShapeIndex, m_MovementDirection, m_MovementScaler,m_HorOrVer,m_45DegreeMovement);
    }
}
