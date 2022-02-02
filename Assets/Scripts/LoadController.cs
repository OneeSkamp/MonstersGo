using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadController : MonoBehaviour {

    public void save() {
                File.AppendAllText(Application.persistentDataPath + "\\Save1.txt", "0" + Environment.NewLine);
    }

    public void load() {
        string[] arStr =  File.ReadAllLines(Application.persistentDataPath + "\\Save1.txt");
        foreach(var str in arStr) {
            Debug.Log(str);
        }
    }
}
