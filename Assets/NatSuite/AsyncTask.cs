using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncTask : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        Debug.LogError("Waiting 1 second...");
        await Task.Delay(TimeSpan.FromSeconds(1));
        Debug.LogError("Done!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
