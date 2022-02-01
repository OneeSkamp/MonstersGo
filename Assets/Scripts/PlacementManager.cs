using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementManager : MonoBehaviour {
    public Camera mainCamera;
    public ARRaycastManager RaycastManager;

    public List<GameObject> rareMonsters;
    public List<GameObject> epicMonsters;
    public List<GameObject> legendaryMonsters;

    public TextAsset saves;

    private float timer = 3f;
    private float duration = 7f;

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
                var randomValue = Random.Range(0, 101);
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

        RaycastHit hit;
        var touchRaycast = Physics.Raycast(mainCamera.ScreenPointToRay(touch.position), out hit);

        if (hit.transform.gameObject != null) {
            if (hit.transform.GetComponent<MonsterController>() != null) {
                var monsterController = hit.transform.GetComponent<MonsterController>();
                // string fileName = "out.txt";
                StreamWriter f = new StreamWriter(Application.persistentDataPath + "\\Save.txt");
                switch (monsterController.rarity) {
                    case Rarity.Rare:
                        f.WriteLine("0");
                        f.Close();
                        // collectionController.rareMonsters.Add(Instantiate(hit.transform.gameObject));
                        break;
                    case Rarity.Epic:
                        f.WriteLine("1");
                        f.Close();
                        // collectionController.epicMonsters.Add(Instantiate(hit.transform.gameObject));
                        break;
                    case Rarity.Legendary:
                        f.WriteLine("2");
                        f.Close();
                        // collectionController.legendaryMonsters.Add(Instantiate(hit.transform.gameObject));
                        break;
                }
                f.Close();
                Destroy(hit.transform.gameObject);
            }
        }
    }

    public void RandomSpawn(List<GameObject> monsters, Pose pose) {
        var rand = Random.Range(0, monsters.Count);
        var quat = new Quaternion(pose.rotation.x, pose.rotation.y + 180f, pose.rotation.z, pose.rotation.w);
        Instantiate(monsters[0], pose.position, quat);
    }
}
