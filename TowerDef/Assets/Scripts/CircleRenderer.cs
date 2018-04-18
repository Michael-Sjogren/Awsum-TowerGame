using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour {
        [Range(0.1f, 100f)]
        public float radius = 20f;

        [Range(3, 256)]
         public int numSegments = 128;
        public void DoRenderer ( ) 
		{
            LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
            Color c1 = new Color(0f, 1f, 0.1f, 0f );
			lineRenderer.material = new Material(Shader.Find("Particles/Standard Surface"));
			lineRenderer.receiveShadows = false;
			lineRenderer.shadowCastingMode = 0;
			lineRenderer.startColor = c1;
			lineRenderer.endColor = c1;
			lineRenderer.startWidth = .05f;
			lineRenderer.endWidth = .05f;
			lineRenderer.positionCount = numSegments + 1;
			lineRenderer.useWorldSpace = false;

         	float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
            float theta = 0f;

            for (int i = 0 ; i < numSegments + 1 ; i++) 
			{

                float x = radius * Mathf.Cos(theta);
                float z = radius * Mathf.Sin(theta);

            	Vector3 pos = new Vector3(x, .05f, z);
                lineRenderer.SetPosition(i, pos);
                theta += deltaTheta;
        	}
    }
}