using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadController : MonoBehaviour {

    public void save() {
                StreamWriter f = new StreamWriter(Application.persistentDataPath + "\\Save1.txt");

                        f.WriteLine("0");
                        f.Close();

    }

    public void load() {
        string[] arStr =  File.ReadAllLines(Application.persistentDataPath + "\\Save1.txt");
        foreach(var str in arStr) {
            Debug.Log(str);
        }
    }
}
