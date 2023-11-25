using System;
using UnityEngine;

namespace AKVA.Interaction
{
    public class MindControlledObject : MonoBehaviour
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        private Mesh defaultMesh;
        private Material defaultMaterial;

        private Rigidbody rb;
        private void Awake()
        {
            meshFilter = GetComponentInChildren<MeshFilter>();
            meshRenderer = GetComponentInChildren<MeshRenderer>();

            defaultMesh = meshFilter.mesh;
            defaultMaterial = meshRenderer.material;
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector3(0, 0, 0);
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
