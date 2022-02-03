using UnityEngine;

public class SwipeRotate : MonoBehaviour {
    private Touch touch;
    private Quaternion rotationY;
    public float speed;

    private void Update() {
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) {
                rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * speed, 0f);
                transform.rotation = rotationY * transform.rotation;
            }
        }
    }
}
