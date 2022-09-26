using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFramework.Editor
{
    [CreateAssetMenu(fileName = "CustomAssemblys", menuName = "GameFramework/CustomAssemblys")]
    public class CustomAssemblys : ScriptableObject
    {
        [Header("自定义程序集")]
        public string[] Assemblys = new string[] { };
    }
}