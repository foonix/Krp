using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using System.Text;

namespace Krp_BepInEx
{
    public class KrpLoaderShim : MonoBehaviour
    {
        public KrpPipelineAsset krpAsset;
        public bool useKrpOnStart;

        private void Awake()
        {
            var current = GraphicsSettings.currentRenderPipeline;
            //Debug.Log(current?.ToString());

            if (krpAsset is null)
            {
                krpAsset = ScriptableObject.CreateInstance<KrpPipelineAsset>();
            }

            if (useKrpOnStart)
            {
                ToggleKrp();
            }

            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            var keyboard = Keyboard.current;
            if (keyboard.altKey.isPressed && keyboard.jKey.wasPressedThisFrame)
            {
                ToggleKrp();
            }
        }

        private void ToggleKrp()
        {
            if (GraphicsSettings.defaultRenderPipeline is null)
            {
                GraphicsSettings.defaultRenderPipeline = krpAsset;
                Debug.Log($"KRP Enabled. Color space: {QualitySettings.activeColorSpace}");
                DumpCameraDataToLog();
            }
            else
            {
                GraphicsSettings.defaultRenderPipeline = null;
                Debug.Log("KRP disabled.");
            }
        }

        private void DumpCameraDataToLog()
        {
            // dump camera list
            foreach (var camera in FindObjectsOfType<Camera>())
            {
                var target = camera.targetTexture is null ? camera.targetDisplay.ToString() : camera.targetTexture.name;
                Debug.Log($"Camera:{camera.name} depth:{camera.depth} target:{target} enabled:{camera.isActiveAndEnabled} mask:{camera.cullingMask}");
                StringBuilder layers = new StringBuilder();
                for (int i = 0; i <= 31; i++)
                {
                    bool enabled = (camera.cullingMask & (1 << i)) > 0;
                    if (enabled)
                    {
                        layers.Append(LayerMask.LayerToName(i));
                        layers.Append(' ');
                    }
                }
                Debug.Log($"LayerNames: {layers}");
            }

            // dump layer list
            for (int i = 0; i <= 31; i++)
            {
                string text = LayerMask.LayerToName(i);
                if (!string.IsNullOrEmpty(text))
                {
                    Debug.Log($"Layer {i}: {text}");
                }
            }
        }
    }
}