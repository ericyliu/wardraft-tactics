using UnityEngine;
using System.Collections;

namespace Wardraft.Game {

  public class TileViewModel : MonoBehaviour {
  
    Renderer modelRenderer;
    Shader normalShader, outlineShader;
    Color normalColor;
    
    void Start () {
      normalShader = Shader.Find("Standard");
      outlineShader = Shader.Find("Standard");
      modelRenderer = transform.GetComponentInChildren<Renderer>();
      normalColor = modelRenderer.material.GetColor("_Color");
    }
    
    public void Normal () {
      //modelRenderer.material.shader = normalShader;
      //modelRenderer.material.SetColor("_Color", normalColor);
      destroyOutlines();
    }
    
    public void Mouseover () {
      //modelRenderer.material.shader = outlineShader;
      //modelRenderer.material.SetColor("_Color", new Color(.9f,.9f,.9f,1f));
      destroyOutlines();
      GameObject outline = Instantiate(ResourceLoader.current.misc["tileMouseoverOutline"]) as GameObject;
      outline.name = "MouseoverOutline";
      outline.transform.SetParent(transform,false);
    }
    
    public void Select () {
      //modelRenderer.material.shader = outlineShader;
      //modelRenderer.material.SetColor("_Color", new Color(1f,.3f,.3f,1f));
      destroyOutlines();
      GameObject outline = Instantiate(ResourceLoader.current.misc["tileSelectionOutline"]) as GameObject;
      outline.name = "SelectionOutline";
      outline.transform.SetParent(transform,false);
    }
    
    void destroyOutlines () {
      Transform selectionOutline = transform.FindChild("SelectionOutline");
      Transform mouseoverOutline = transform.FindChild("MouseoverOutline");
      if (selectionOutline != null) Object.Destroy(selectionOutline.gameObject);
      if (mouseoverOutline != null) Object.Destroy(mouseoverOutline.gameObject);
    }
  
  }

}