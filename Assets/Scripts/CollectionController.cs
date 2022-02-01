using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CollectionController : MonoBehaviour {
    public Transform monsterPos;
    public Transform collectionTransform;
    public Button monsterButton;

    public List<GameObject> allMonsters;

    public List<GameObject> rareMonsters;
    public List<GameObject> epicMonsters;
    public List<GameObject> legendaryMonsters;

    public GameObject currentMonster;

    public void FillCollection() {
        string[] arStr =  File.ReadAllLines(Application.persistentDataPath + "\\Save.txt");

        foreach (var str in arStr) {
            switch (str) {
                case "0":
                    rareMonsters.Add(Instantiate(allMonsters[0]));
                    break;
                case "1":
                    rareMonsters.Add(Instantiate(allMonsters[1]));
                    break;
                case "2":
                    rareMonsters.Add(Instantiate(allMonsters[2]));
                    break;
            }
        }

        FillCollectionFromList(legendaryMonsters);
        FillCollectionFromList(epicMonsters);
        FillCollectionFromList(rareMonsters);
    }

    public void FillCollectionFromList(List<GameObject> monsters) {
        if (monsters == null) return;
        foreach(var monster in monsters) {
            var monsterController = monster.GetComponent<MonsterController>();
            var btn = Instantiate(monsterButton, collectionTransform);
            btn.image.sprite = monsterController.icon;
            btn.onClick.AddListener(() => SpawnMonster(monster));
            // monsterController.enabled = false;
            // FileStream aFile = new FileStream(Application.persistentDataPath + "Save.txt", FileMode.OpenOrCreate);
            // StreamReader sw = new StreamReader(aFile);
            // StreamReader sr = new StreamReader(Application.persistentDataPath + "Save.txt");
            // while (!sr.EndOfStream) {
            // // line = sr.ReadLine();
            // //бла-бла-бла
            // }
            // sr.Close();
        }
    }

    public void CleanCollection() {
        foreach (Transform item in collectionTransform) {
            Destroy(item.gameObject);
        }
    }

    public void SpawnMonster(GameObject monster) {
        if (monster != currentMonster && monster != null) {
            Destroy(currentMonster);
            currentMonster = Instantiate(monster, monsterPos.position, monsterPos.rotation);
        }
    }
}
