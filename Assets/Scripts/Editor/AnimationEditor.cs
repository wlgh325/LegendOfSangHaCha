using UnityEngine;
using UnityEditor;

public class AnimationEditor : MonoBehaviour
{
    /*
    [MenuItem("Assets/Export Animation", false, 8)]
    public static void ExportSelectedAnimation()
    {
        foreach (UnityEngine.Object obj in Selection.objects)
        {
            System.Object type = obj.GetType();
            if (obj is AnimationClip == false)
                continue;

            string oldPath = AssetDatabase.GetAssetPath(obj);



            string animationName = obj.name;
            int extentionStartIndex = oldPath.LastIndexOf('.');
            oldPath = oldPath.Substring(0, extentionStartIndex);
            string newPath = string.Format("{0}__{1}.anim", oldPath, animationName);

            //기존 경로에 AnimationClip이 존재하면 파일을 새로 만들지 않고 기존 파일에다가 정보를 쓴다.

            if (type == typeof(AnimationClip))
            {
                AnimationClip animationClip = AssetDatabase.LoadAssetAtPath(newPath, typeof(AnimationClip)) as AnimationClip;
                if (animationClip == null)
                {
                    animationClip = new AnimationClip();
                    EditorUtility.CopySerialized(obj, animationClip);
                    AssetDatabase.CreateAsset(animationClip, newPath);
                }
                else
                {
                    EditorUtility.CopySerialized(obj, animationClip);
                }
            }
        }
        AssetDatabase.SaveAssets();
    }
    */

}
