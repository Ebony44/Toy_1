#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
// using System.Net;
using System.Net.Http;
using UnityEditor;
using UnityEngine;


public class FileDownloader : EditorWindow
{

    private void OnGUI()
    {
        
    }

    public void OnDownloadButton()
    {
        using (var client = new HttpClient())
        {
            using (var s = client.GetStreamAsync("https://via.placeholder.com/150"))
            {
                using (var fs = new FileStream("localfile.jpg", FileMode.OpenOrCreate))
                {
                    s.Result.CopyTo(fs);
                }
            }
        }

        //using var client = new HttpClient();
        //using var s = await client.GetStreamAsync("https://via.placeholder.com/150");
        //using var fs = new FileStream("localfile.jpg", FileMode.OpenOrCreate);
        //await s.CopyToAsync(fs);
    }
    public async void OnDownloadButtonAsync()
    {
        using var client = new HttpClient();
        using var s = await client.GetStreamAsync("https://via.placeholder.com/150");
        using var fs = new FileStream("localfile.jpg", FileMode.OpenOrCreate);
        await s.CopyToAsync(fs);
    }



}
#endif