using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 20;
    public float maxY = 10;
    public float maxX = 10;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (
                Mathf.Abs((Camera.main.transform.position + direction).x) + Camera.main.orthographicSize < maxX
                &&
                Mathf.Abs((Camera.main.transform.position + direction).y) + Camera.main.orthographicSize < maxY
                )
            {
                Camera.main.transform.position += direction;
            }
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        float tmp = Camera.main.orthographicSize;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        if (tmp != Camera.main.orthographicSize && Camera.main.orthographicSize > 9)
        {
            foreach (GameObject marker in GameObject.FindGameObjectsWithTag("Marker"))
            {
                if (marker.gameObject.name != "MenuButton")
                {
                    Vector3 newScale = new Vector3(
                                        marker.gameObject.transform.localScale.x + (-increment * 0.01f),
                                        marker.gameObject.transform.localScale.y + (-increment * 0.01f),
                                        marker.gameObject.transform.localScale.z);
                    marker.gameObject.transform.localScale = newScale;
                }
            }
        }
    }
}
