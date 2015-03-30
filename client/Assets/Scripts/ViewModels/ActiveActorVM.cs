using UnityEngine;
using System.Collections;

public class ActiveActorVM : MonoBehaviour {

  Renderer modelRenderer;
  Shader normalShader, outlineShader;
  
  void Start () {
    normalShader = Shader.Find("Standard");
    outlineShader = Shader.Find("Outlined/Diffuse");
    modelRenderer = transform.GetComponentInChildren<Renderer>();
  }
  
  public void Normal () {
    modelRenderer.material.shader = normalShader;
  }
  
  public void MouseoverOwn () {
    modelRenderer.material.shader = outlineShader;
    modelRenderer.material.SetColor("_OutlineColor", new Color(.5f,1f,.5f,.5f));
    
  }
  
  public void MouseoverEnemy () {
    modelRenderer.material.shader = outlineShader;
    modelRenderer.material.SetColor("_OutlineColor", new Color(1f,.5f,.5f,.5f));
  }
  
  public void SelectOwn () {
    modelRenderer.material.shader = outlineShader;
    modelRenderer.material.SetColor("_OutlineColor", new Color(0f,1f,0f,.5f));
  }
  
  public void SelectEnemy () {
    modelRenderer.material.shader = outlineShader;
    modelRenderer.material.SetColor("_OutlineColor", new Color(1f,0f,0f,.5f));
  }
  

}
