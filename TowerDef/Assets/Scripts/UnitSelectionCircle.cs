using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionCircle : MonoBehaviour {

    [SerializeField]
	private float radius = 1f;
    public float Radius {
        get
        {
            return Radius;
        }
        set
        {
            radius = value;
            UpdateCircle();
        }
    }
	public float fieldOfView = 90;
	public float farClipRange = 5f;
	public float nearClipRange = .1f;
	public float heightOffset = .5f;
	public Material circleMaterial;
	public LayerMask ignoreLayers;
	private Projector projector;
	private GameObject projectorObject;
	void Start ()
    {
        projectorObject = new GameObject("UnitCircle");
        projectorObject.transform.SetParent(this.transform);
        projectorObject.transform.localPosition = new Vector3(0, heightOffset, 0);
        projectorObject.transform.localRotation = Quaternion.AngleAxis(90f, Vector3.right);
        projectorObject.AddComponent(typeof(Projector));
        projector = projectorObject.GetComponent<Projector>();
        projectorObject.SetActive(false);

        projector.aspectRatio = 1;
        projector.orthographic = true;
        UpdateCircle();
    }

    public void UpdateCircle()
    {
        projector.orthographicSize = radius;
        projector.material = circleMaterial;
        projector.farClipPlane = farClipRange;
        projector.nearClipPlane = nearClipRange;
        projector.ignoreLayers = ignoreLayers;
    }


    public void DisableCiricle()
	{
		projectorObject.SetActive(false);
	}

	public void EnableCiricle()
	{
		projectorObject.SetActive(true);
	}
	
}
