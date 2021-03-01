/* 
*   NatCorder
*   Copyright (c) 2020 Yusuf Olokoba
*/

namespace NatSuite.Examples {

    using UnityEngine;
    using System.Collections;
    using Recorders;
    using Recorders.Clocks;
    using Recorders.Inputs;
    using UnityEngine.UI;
    using System.IO;
    using NatSuite.Sharing;
    using System;

    public class VideoRecorder : MonoBehaviour {

        [Header(@"Recording")]
        public int videoWidth = 1280;
        public int videoHeight = 720;
        public bool recordMicrophone;
        public GameObject VideoRecordCanvasPenalObject;
        private IMediaRecorder recorder;
        private CameraInput cameraInput;
        private AudioInput audioInput;
        public  GameObject characterLoadAnimationManagerInMicrophoneSource;
        public static bool stopSave=false;
        private IEnumerator Start () {


            // Start microphone
            characterLoadAnimationManagerInMicrophoneSource.AddComponent<AudioSource>();
            characterLoadAnimationManagerInMicrophoneSource.GetComponent<AudioSource>().mute = true;
            characterLoadAnimationManagerInMicrophoneSource.GetComponent<AudioSource>().loop = true;
            characterLoadAnimationManagerInMicrophoneSource.GetComponent<AudioSource>().bypassEffects =
            characterLoadAnimationManagerInMicrophoneSource.GetComponent<AudioSource>().bypassListenerEffects = false;
            characterLoadAnimationManagerInMicrophoneSource.GetComponent<AudioSource>().clip = CharacterLoadAnimation.backgroundClipAudio;
            characterLoadAnimationManagerInMicrophoneSource.GetComponent<AudioSource>().Play();
            yield return new WaitUntil(() => Microphone.GetPosition(null) > 0);
        }

        private void OnDestroy () {
            // Stop microphone
            characterLoadAnimationManagerInMicrophoneSource.GetComponent<AudioSource>().Stop();
            Microphone.End(null);
        }

        public void StartRecording ()
        {
            PlayerPrefs.SetString(ConstantsGod.UPLOADVIDEOPATH, "");
            PlayerPrefs.SetString(ConstantsGod.VIDEOPATH, "");
            videoHeight = (int)VideoRecordCanvasPenalObject.GetComponent<Canvas>().GetComponent<RectTransform>().rect.height;
            videoWidth = (int)VideoRecordCanvasPenalObject.GetComponent<Canvas>().GetComponent<RectTransform>().rect.width;
            
            var frameRate = 30;
            var sampleRate = recordMicrophone ? AudioSettings.outputSampleRate : 0;
            var channelCount = recordMicrophone ? (int)AudioSettings.speakerMode : 0;
            var clock = new RealtimeClock();
            recorder = new MP4Recorder(videoWidth, videoHeight, frameRate, sampleRate, channelCount);
            // Create recording inputs
            cameraInput = new CameraInput(recorder, clock, Camera.main);
            audioInput = recordMicrophone ? new AudioInput(recorder, clock, characterLoadAnimationManagerInMicrophoneSource.GetComponent<AudioSource>(), true) : null;
            // Unmute microphone
            characterLoadAnimationManagerInMicrophoneSource.GetComponent<AudioSource>().mute = audioInput == null;
        }

        public async void StopRecording () {
            
            audioInput?.Dispose();
            cameraInput.Dispose();
            var path = await recorder.FinishWriting();
            // Playback recording
            Debug.Log($"Saved recording to: {path}");
            string filename = Path.GetFileName(path);
            PlayerPrefs.SetString(ConstantsGod.VIDEOPATH, filename);
         // Handheld.PlayFullScreenMovie($"file://{path}");
        }

        public async void StopRecordingForSave()
        {
            stopSave = true;

            audioInput?.Dispose();
            cameraInput.Dispose();
            var path = await recorder.FinishWriting();
            // Playback recording
            Debug.Log($"Uploading recording to: {path}");
            string filename = Path.GetFileName(path);
            PlayerPrefs.SetString(ConstantsGod.UPLOADVIDEOPATH, filename);
            //Handheld.PlayFullScreenMovie($"file://{path}");
        }

        public async void ShareVideo()
        {
            var mes = "";
            try
            {
                var success = await new SharePayload()
                    .AddText("User shared video!")
                    .AddMedia(Application.persistentDataPath + "/abc.mp4")
                    .Commit();

                mes = $"Successfully shared items: {success}";
            }
            catch (Exception e)
            {
                mes = e.Message;
            }
        }

        private IEnumerator copyPath(string path)
        {
            Debug.Log("Trying to create file");

            // Get path of file in streaming assets
            string filePath = path;

            if (File.Exists(filePath))
            {
                WWW www = new WWW(filePath);
                yield return www;
                Debug.LogWarning("File size in bytes: " + www.bytesDownloaded); // Returns 0
                // Write down the file using the byte array
                File.WriteAllBytes(Application.persistentDataPath + "/abc.mp4" , www.bytes);
                Debug.LogError("file path: " + Application.persistentDataPath);
                
            }
            else
                Debug.LogError("Infofile does not exist: " + filePath);
        }
    }

   
}