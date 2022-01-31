using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementManager : MonoBehaviour {
    public Camera mainCamera;
    public ARRaycastManager RaycastManager;
    public GameObject spawnObj;

    public Text text;

    private float timer = 3f;
    private float duration = 7f;

    private int points = 0;

        void Update() {
            timer += Time.deltaTime;

            var hits = new List<ARRaycastHit>();
            var raycast = RaycastManager.Raycast(
                new Vector2(Screen.width / 2, Screen.height / 2),
                hits,
                TrackableType.Planes
            );

            if (hits.Count > 0) {
                if (timer > duration) {
                    Instantiate(spawnObj, hits[0].pose.position, hits[0].pose.rotation);
                    timer = 0;
                }
            }

            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) {
                return;
            }

            RaycastHit hit;
            var touchRaycast = Physics.Raycast(mainCamera.ScreenPointToRay(touch.position), out hit);

            if (hit.transform.gameObject != null) {
                if (hit.transform.GetComponent<MonsterController>() != null) {
                    points++;
                    text.text = points.ToString();
                    Destroy(hit.transform.gameObject);
                }
            }
        }

}
