using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomInZoomOut : MonoBehaviour {

    public Image sizeable;

	float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

	Vector2 firstTouchPrevPos, secondTouchPrevPos;

	[SerializeField]
	float zoomModifierSpeed = 0.1f;

	[SerializeField]
	Text text;
	
	// Update is called once per frame
	void Update () {
		
		if (Input.touchCount == 2) {
			Touch firstTouch = Input.GetTouch (0);
			Touch secondTouch = Input.GetTouch (1);

			firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
			secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

			touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
			touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

			zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
            {
                sizeable.rectTransform.localScale += new Vector3 (zoomModifier,zoomModifier,1);
            }

            if (touchesPrevPosDifference < touchesCurPosDifference)
            {
                sizeable.rectTransform.localScale -= new Vector3(zoomModifier, zoomModifier, 1);
            }
			
		}

		//mainCamera.orthographicSize = Mathf.Clamp (mainCamera.orthographicSize, 2f, 10f);
		//text.text = "Camera size " + mainCamera.orthographicSize;

	}
}
