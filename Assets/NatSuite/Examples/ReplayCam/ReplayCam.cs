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

    public class ReplayCam : MonoBehaviour {

        [Header(@"Recording")]
        public int videoWidth = 1280;
        public int videoHeight = 720;
        public bool recordMicrophone;
        public Text pathText;

        private IMediaRecorder recorder;
        private CameraInput cameraInput;
        private AudioInput audioInput;
        private AudioSource microphoneSource;
       // public Button startBtn;
       // public Button stopBtn;
        private IEnumerator Start () {
            // Start microphone
            microphoneSource = gameObject.AddComponent<AudioSource>();
            microphoneSource.mute =
            microphoneSource.loop = true;
            microphoneSource.bypassEffects =
            microphoneSource.bypassListenerEffects = false;
           // microphoneSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
            microphoneSource.clip = AB.clipAudio;
            yield return new WaitUntil(() => Microphone.GetPosition(null) > 0);
            microphoneSource.Play();

        }

        private void OnDestroy () {
            // Stop microphone
            microphoneSource.Stop();
            Microphone.End(null);
        }

        public void StartRecording () {
            //startBtn.interactable = false;
            //stopBtn.interactable = true;
            // Start recording
            var frameRate = 30;
            var sampleRate = recordMicrophone ? AudioSettings.outputSampleRate : 0;
            var channelCount = recordMicrophone ? (int)AudioSettings.speakerMode : 0;
            var clock = new RealtimeClock();
            recorder = new MP4Recorder(videoWidth, videoHeight, frameRate, sampleRate, channelCount);
            // Create recording inputs
            cameraInput = new CameraInput(recorder, clock, Camera.main);
            audioInput = recordMicrophone ? new AudioInput(recorder, clock, microphoneSource, true) : null;
            // Unmute microphone
            microphoneSource.mute = audioInput == null;
        }

        public async void StopRecording () {
            //stopBtn.interactable = false;
            //startBtn.interactable = true;
            // Mute microphone
            microphoneSource.mute = true;
            // Stop recording
            audioInput?.Dispose();
            cameraInput.Dispose();
            var path = await recorder.FinishWriting();
            // Playback recording
            Debug.Log($"Saved recording to: {path}");
            pathText.text = path.ToString();
            StartCoroutine(copyPath(path));
           
           // Handheld.PlayFullScreenMovie($"file://{path}");
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
            Debug.Log("Searching for file in streaming assets " + filePath);

            if (File.Exists(filePath))
            {
                Debug.Log("File found! Now trying to write down bytes in new file.");

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