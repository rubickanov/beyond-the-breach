using AKVA.Assets.Vince.Scripts.AI;
using UnityEngine;

namespace AKVA.Interaction
{
    public class MindControlledObject : MonoBehaviour
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private SkinnedMeshRenderer skinnedMeshRenderer;

        public Mesh defaultMesh;
        public Material defaultMaterial;

        private void Awake()
        {
            skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            //meshFilter = GetComponentInChildren<MeshFilter>();
            //meshRenderer = GetComponentInChildren<MeshRenderer>();

            //defaultMesh = meshFilter.mesh;
            //defaultMaterial = meshRenderer.material;
            defaultMesh = skinnedMeshRenderer.sharedMesh;
            defaultMaterial = skinnedMeshRenderer.material;
        }

        public void TakePlayerAppearance(Mesh mesh, Material material)
        {
            gameObject.GetComponent<ScientistBT>()?.SetMindControl(true);
            //meshFilter.mesh = mesh;
            //meshRenderer.material = material;
            skinnedMeshRenderer.sharedMesh = mesh;
            skinnedMeshRenderer.material = material;
        }

        public void ResetAppearance()
        {
            //meshFilter.mesh = defaultMesh;
            //meshRenderer.material = defaultMaterial;
            gameObject.GetComponent<ScientistBT>()?.SetMindControl(false);
            skinnedMeshRenderer.sharedMesh = defaultMesh;
            skinnedMeshRenderer.material = defaultMaterial;
        }
    }
}
