using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementManager : MonoBehaviour {
    public Camera mainCamera;
    public ARRaycastManager raycastManager;

    public List<GameObject> rareMonsters;
    public List<GameObject> epicMonsters;
    public List<GameObject> legendaryMonsters;

    public TextAsset saves;

    private float timer = 3f;
    private float duration = 7f;

    void Update() {
        timer += Time.deltaTime;

        var hits = new List<ARRaycastHit>();
        var raycast = raycastManager.Raycast(
            new Vector2(Screen.width / 2, Screen.height / 2),
            hits,
            TrackableType.PlaneWithinPolygon
        );

        if (hits.Count > 0) {
            if (timer > duration) {
                var randomValue = UnityEngine.Random.Range(0, 101);
                if (randomValue < 70) {
                    RandomSpawn(rareMonsters, hits[0].pose);
                }

                if (randomValue > 69 && randomValue < 98) {
                    RandomSpawn(epicMonsters, hits[0].pose);
                }

                if (randomValue > 97 && randomValue < 101) {
                    RandomSpawn(legendaryMonsters, hits[0].pose);
                }

                timer = 0;
            }
        }

        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) {
            return;
        }

        if (Physics.Raycast(mainCamera.ScreenPointToRay(touch.position), out RaycastHit hit)) {
            if (hit.transform.GetComponent<MonsterController>() != null) {
                var monsterController = hit.transform.GetComponent<MonsterController>();
                var animator = hit.transform.GetComponent<Animator>();
                animator.SetBool("TakeDmg", true);
                animator.SetBool("takeDmg", false);

                var monsterType = "";
                switch (monsterController.rarity) {
                    case Rarity.Rare:
                        monsterType = "0";
                        break;
                    case Rarity.Epic:
                        monsterType = "1";
                        break;
                    case Rarity.Legendary:
                        monsterType = "2";
                        break;
                }

                File.AppendAllText(
                   Path.Combine(Application.persistentDataPath, "Save.txt"),
                    monsterType + Environment.NewLine
                );
            }
        }
    }

    public void RandomSpawn(List<GameObject> monsters, Pose pose) {
        var rand = UnityEngine.Random.Range(0, monsters.Count);
        var quat = new Quaternion(
            pose.rotation.x,
            pose.rotation.y + 180f,
            pose.rotation.z,
            pose.rotation.w
        );

        Instantiate(monsters[0], pose.position, quat);
    }
}
