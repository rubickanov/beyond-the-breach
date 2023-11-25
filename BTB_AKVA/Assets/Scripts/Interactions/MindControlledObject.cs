using AKVA.Assets.Vince.Scripts.AI;
using UnityEngine;

namespace AKVA.Interaction
{
    public class MindControlledObject : MonoBehaviour
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        private Mesh defaultMesh;
        private Material defaultMaterial;

        private void Awake()
        {
            meshFilter = GetComponentInChildren<MeshFilter>();
            meshRenderer = GetComponentInChildren<MeshRenderer>();

            defaultMesh = meshFilter.mesh;
            defaultMaterial = meshRenderer.material;
        }

        public void TakePlayerAppearance(Mesh mesh, Material material)
        {
            meshFilter.mesh = mesh;
            meshRenderer.material = material;
        }

        public void ResetAppearance()
        {
            meshFilter.mesh = defaultMesh;
            meshRenderer.material = defaultMaterial;
        }
    }
}
