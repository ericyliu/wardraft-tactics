using UnityEngine;
using System.Collections;

namespace Wardraft.Game {

  public class TileViewModel : MonoBehaviour {
  
    Renderer modelRenderer;
    Shader normalShader, outlineShader;
    
    void Start () {
      normalShader = Shader.Find("Diffuse");
      outlineShader = Shader.Find("Outlined/Diffuse");
      modelRenderer = transform.GetComponentInChildren<Renderer>();
    }
    
    public void Normal () {
      modelRenderer.material.shader = normalShader;
    }
    
    public void Mouseover () {
      modelRenderer.material.shader = outlineShader;
      modelRenderer.material.SetColor("_OutlineColor", new Color(1f,1f,1f,1f));
      
    }
    
    public void Select () {
      modelRenderer.material.shader = outlineShader;
      modelRenderer.material.SetColor("_OutlineColor", new Color(.5f,1f,1f,1f));
    }
  
  }

}