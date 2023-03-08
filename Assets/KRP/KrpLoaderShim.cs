using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

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
                GraphicsSettings.defaultRenderPipeline = krpAsset;
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
            }
            else
            {
                GraphicsSettings.defaultRenderPipeline = null;
            }
        }
    }
}