﻿using UnityEngine;
using System.Collections;
 
public class HiResScreenShots : MonoBehaviour {
     public int resWidth = 7680; 
     public int resHeight = 4320;
 
     private bool takeHiResShot = false;
 
     public static string ScreenShotName(int width, int height) {
         return string.Format("{0}/screen_{1}x{2}_{3}.png", 
         //return string.Format("C:/Users/danie/Desktop/screen_{1}x{2}_{3}.png",
                              Application.dataPath, 
                              width, height, 
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
     }
 
     public void TakeHiResShot() {
         takeHiResShot = true;
     }
 
     void LateUpdate() {
         takeHiResShot = Input.GetKeyDown("k");
         if (takeHiResShot) {
			 Debug.Log("Start");
			 Camera cam = GetComponent<Camera>();
             RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
             cam.targetTexture = rt;
             Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
             cam.Render();
             RenderTexture.active = rt;
             screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
             cam.targetTexture = null;
             RenderTexture.active = null; // JC: added to avoid errors
             Destroy(rt);
             byte[] bytes = screenShot.EncodeToPNG();
             string filename = ScreenShotName(resWidth, resHeight);
             System.IO.File.WriteAllBytes(filename, bytes);
             Debug.Log(string.Format("Took screenshot to: {0}", filename));
             takeHiResShot = false;
         }
     }
 }