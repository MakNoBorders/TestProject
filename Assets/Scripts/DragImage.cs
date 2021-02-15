using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragImage : MonoBehaviour , IDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;

  

    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.LogError("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }


    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    Debug.LogError("OnBeginDrag");
    //}
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.LogError("OnEndDrag");
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.LogError("OnPointerDown");
    //}
}
