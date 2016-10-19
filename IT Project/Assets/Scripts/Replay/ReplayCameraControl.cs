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
				Vector3 pos = new Vector3 (0, 0, 0);
				pos.x = this.transform.position.x + movement.x * SlideSpeed;
				pos.y = this.transform.position.y;
				pos.z = this.transform.position.z + movement.y * SlideSpeed;
				Mathf.Clamp (pos.x, 0, 900f);
				Mathf.Clamp (pos.z, 0, 900f);
				this.transform.position = pos;
//                this.transform.Translate(new Vector3(
//                    movement.x * SlideSpeed,
//                    0f,
//                    movement.y * SlideSpeed
//                ));
            }
            lastSlidePoint = touchPos;

        } else if (Input.touchCount == 2) {
            float dist = Vector2.Distance(
                    Input.touches[0].position,
                    Input.touches[1].position
            );

            if (lastZoomDist != null) {
				Vector3 pos = new Vector3 (0, 0, 0);
				pos.x = this.transform.position.x;
				pos.y = this.transform.position.y - ((dist - (float)lastZoomDist) * ZoomSpeed);
				pos.z = this.transform.position.z;
				Mathf.Clamp (pos.y, 100f, 700f);
				this.transform.position = pos;
            }

            lastZoomDist = dist;
        } else {
            lastSlidePoint = null;
            lastZoomDist = null;
        }
    }
}
