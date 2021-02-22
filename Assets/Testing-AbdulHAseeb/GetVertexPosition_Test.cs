using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVertexPosition_Test : MonoBehaviour
{
    public GameObject m_Obj_Prefab;
    public GameObject m_Container;

    public Vector3 m_OffSet;


    Mesh m_Mesh;

    
    void Start()
    {
        m_Mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        //LoadObjectsOnVertexPositions();

        //Vector3 spawnposition = transform.position + transform.InverseTransformPoint(m_Mesh.vertices[m_Mesh.triangles[PlayerPrefs.GetInt("HitAngle") * 3 + 1]]);
        //GameObject l_Obj = (GameObject)Instantiate(m_Obj_Prefab);
        //l_Obj.transform.position = spawnposition;// new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
    }

    void LoadObjectsOnVertexPositions()
    {
        for(int i=0;i<m_Mesh.vertexCount;i++)
        {
            GameObject l_Obj = (GameObject)Instantiate(m_Obj_Prefab);
            l_Obj.transform.position = m_Mesh.vertices[i];
            l_Obj.transform.parent = m_Container.transform;
        }

        m_Container.transform.position = transform.position;
        m_Container.transform.rotation = transform.rotation;
    }


    private void Update()
    {
        GetHitTriangleIndex(m_Obj_Prefab);
    }

    public void GetHitTriangleIndex(GameObject l_Obj_Prefab)
    {
        RaycastHit l_Hit;
        Ray l_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(l_Ray, out l_Hit))
            {
                Vector3 l_SpawnPosition = transform.position + transform.InverseTransformPoint(m_Mesh.vertices[m_Mesh.triangles[l_Hit.triangleIndex*3 + 1]]) + m_OffSet;
                GameObject l_Obj = (GameObject)Instantiate(l_Obj_Prefab);
                l_Obj.transform.position = l_SpawnPosition;
                print(l_Hit.triangleIndex.ToString());
            }
        }
    }
}
