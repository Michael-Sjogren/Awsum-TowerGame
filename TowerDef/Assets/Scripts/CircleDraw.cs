using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class CircleDraw : MonoBehaviour 
{   
  public float theta_scale = 0.005f;        //Set lower to add more points
  public int size = 16; //Total number of points in circle
  public float radius = 6f;
  public float startWidth = .03f;
  public float endtWidth = .03f;
  private LineRenderer lineRenderer;

  void Awake () 
  {       
    float sizeValue = (2.0f * Mathf.PI) / theta_scale; 
    size = (int)sizeValue;
    size++;
    lineRenderer = gameObject.AddComponent<LineRenderer>();
    lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
	lineRenderer.startWidth = startWidth;
	lineRenderer.endWidth = endtWidth;
	lineRenderer.shadowCastingMode = ShadowCastingMode.Off;
    lineRenderer.positionCount = size;
	DrawCiricle();      
  }


  public void DrawCiricle()
  {
	Vector3 pos;
    float theta = 0f;
    for(int i = 0; i < size; i++)
	{          
      theta += (2.0f * Mathf.PI * theta_scale);         
      float x = radius * Mathf.Cos(theta);
      float z = radius * Mathf.Sin(theta);          
      x += gameObject.transform.position.x;
      z += gameObject.transform.position.z;
      pos = new Vector3(x, gameObject.transform.position.y , z);
      lineRenderer.SetPosition(i, pos);
    }
  }
}