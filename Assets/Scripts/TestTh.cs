using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TestTh : MonoBehaviour
{
    public float[] randomList;

    void Run()
    {
        randomList = new float[1000000];
        Thread t = new Thread(delegate()
        {
            while(true)
            {
                Generate();
                Thread.Sleep(16); // trigger the loop to run roughly every 60th of a second
            }            
        });
        t.Start();
    }

    void Generate()
    {
      System.Random rnd = new System.Random();
      for(int i=0;i<randomList.Length;i++) randomList[i] = (float)rnd.NextDouble();
    }
}
