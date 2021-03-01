/* 
*   NatCorder
*   Copyright (c) 2020 Yusuf Olokoba.
*/

namespace NatSuite.Recorders.Internal {

    using UnityEngine;
    using System;
    using System.IO;

    public static class Utility {

        private static string directory;

        public static string GetPath (string extension) {

            if (CharacterLoadAnimation.saveVideoBtnClick)
            {
                CharacterLoadAnimation.saveVideoBtnClick = false;
                if (directory == null)
                {
                    Debug.LogError("111111111" + CharacterLoadAnimation.saveVideoBtnClick);

                    Debug.LogError("22222222" + CharacterLoadAnimation.saveVideoBtnClick);
                    if (!Directory.Exists(Application.persistentDataPath + "/SaveVideo"))
                    {
                        Debug.LogError("333333" + CharacterLoadAnimation.saveVideoBtnClick);
                        Directory.CreateDirectory(Application.persistentDataPath + "/" + "SaveVideo");
                    }
                    var editor = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
                    directory = editor ? Application.persistentDataPath + "/SaveVideo" : Application.persistentDataPath + "/SaveVideo";
                }
                else
                {
                  

                    Debug.LogError("44444444" + CharacterLoadAnimation.saveVideoBtnClick);
                    if (!Directory.Exists(Application.persistentDataPath + "/SaveVideo"))
                    {
                        Debug.LogError("5555555" + CharacterLoadAnimation.saveVideoBtnClick);
                        Directory.CreateDirectory(Application.persistentDataPath + "/" + "SaveVideo");
                    }
                    var editor = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
                    directory = editor ? Application.persistentDataPath + "/SaveVideo" : Application.persistentDataPath + "/SaveVideo";
                }
                var timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
                var name = $"recording_{timestamp}{extension}";
                var path = Path.Combine(directory, name);
                return path;
            }
            else
            {
                if (directory == null)
                {
                    if (!Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                    {
                        Debug.LogError("5555555" + CharacterLoadAnimation.saveVideoBtnClick);
                        Directory.CreateDirectory(Application.persistentDataPath + "/" + "UploadVideo");
                    }
                    var editor = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
                    directory = editor ? Application.persistentDataPath+ "/UploadVideo" : Application.persistentDataPath+ "/UploadVideo";
                }
                else
                {
                    if (!Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                    {
                        Debug.LogError("5555555" + CharacterLoadAnimation.saveVideoBtnClick);
                        Directory.CreateDirectory(Application.persistentDataPath + "/" + "UploadVideo");
                    }
                    var editor = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
                    directory = editor ? Application.persistentDataPath+ "/UploadVideo" : Application.persistentDataPath+ "/UploadVideo";
                }
                var timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
                var name = $"recording_{timestamp}{extension}";
                var path = Path.Combine(directory, name);
                return path;
            }
           
        }


        
    }
}