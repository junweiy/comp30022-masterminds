using UnityEngine;
using System.Collections;

public class ReplayCameraControl : MonoBehaviour {

    public float SlideSpeed = 5f;
    public float ZoomSpeed = 5f;

    Vector2? lastSlidePoint;
    float? lastZoomDist;


    // Update is called once per frame
    void Update() {
        if (Input.touchCount == 1) {
            Vector2 touchPos = Input.touches[0].position;
            if (lastSlidePoint != null) {
                var movement = (Vector2)lastSlidePoint - Input.touches[0].position;
                this.transform.Translate(new Vector3(
                    movement.x * SlideSpeed,
                    0f,
                    movement.y * SlideSpeed
                ));
            }
            lastSlidePoint = touchPos;

        } else if (Input.touchCount == 2) {
            float dist = Vector2.Distance(
                    Input.touches[0].position,
                    Input.touches[1].position
            );

            if (lastZoomDist != null) {

                this.GetComponent<Camera>().fieldOfView -= (dist - (float)lastZoomDist) * ZoomSpeed;
            }

            lastZoomDist = dist;
        } else {
            lastSlidePoint = null;
            lastZoomDist = null;
        }
    }
}
