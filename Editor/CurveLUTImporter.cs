using UnityEditor;
using UnityEngine;
using System.IO;

#if UNITY_2020_2_OR_NEWER
using UnityEditor.AssetImporters;
#else
using UnityEditor.Experimental.AssetImporters;
#endif

namespace CurveLUT
{
    [ScriptedImporter(1, "curveLUT")]
    public class CurveLUTImporter : ScriptedImporter
    {
        public AnimationCurve curveA = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public AnimationCurve curveB = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public AnimationCurve curveC = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public AnimationCurve curveD = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        public bool autoApply;
        
        public override void OnImportAsset(AssetImportContext context)
        {
            Texture2D lut = CurveLUT.Create(curveA, curveB, curveC, curveD);

            context.AddObjectToAsset("Look up Texture", lut);
            context.SetMainObject(lut);
        }
        
        [MenuItem("Assets/Create/Curve LUT")]
        private static void CreateCurveLUTAsset()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New Curve LUT.curveLUT");

            File.Create(assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Texture2D lut = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPathAndName);

            Selection.activeObject = lut;
        }
    }
    
    [CustomEditor(typeof(CurveLUTImporter))]
    public class CubeImporterEditor: ScriptedImporterEditor
    {
        private CurveLUTImporter importer;

        public override void OnEnable()
        {
            importer = (CurveLUTImporter)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (importer.autoApply && HasModified())
            {
                this.Apply();
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this.assetTarget));
            }
        }
    }
}