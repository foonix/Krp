using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ModInitializerScriptableObject", order = 1)]
public class ModInitializer : ScriptableObject, IObjectInitializationDataProvider
{
    public string initMessage = "Hello KSP2";

    public string Name => "Foonix's Test Mod Mod Initializer";

    public ObjectInitializationData CreateObjectInitializationData()
    {
        Debug.LogWarning(initMessage);
        throw new System.NotImplementedException();
    }
}
