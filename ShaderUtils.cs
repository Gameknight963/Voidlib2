using UnityEngine;
using static Il2CppRootMotion.FinalIK.GrounderQuadruped;

namespace VoidLib2
{
    public static class ShaderUtils
    {
        /// <summary>
        /// Sets the shader of all MeshRenderers in the GameObject and its children.
        /// </summary>
        /// <param name="root">The root GameObject.</param>
        /// <param name="shader">The shader to assign.</param>
        public static void SetShaderRecursive(GameObject root, Shader shader)
        {
            MeshRenderer[] renderers = root.GetComponentsInChildren<MeshRenderer>(true);

            foreach (MeshRenderer mr in renderers)
            {
                Material[] mats = mr.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i].shader = shader;
                }
            }
        }

        /// <summary>
        /// Just for fun lol
        /// </summary>
        /// <param name="root">Root gameobject</param>
        /// <param name="color">Color to assign</param>
        public static void SetColorRecursive(GameObject root, Color color)
        {
            MeshRenderer[] renderers = root.GetComponentsInChildren<MeshRenderer>(true);

            foreach (MeshRenderer mr in renderers)
            {
                Material[] mats = mr.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i].color = color;
                }
            }
        }
    }
}
