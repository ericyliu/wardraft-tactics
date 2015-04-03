using UnityEngine;
using System.Collections;

namespace Wardraft.Game {

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
  
  public void PlayAnimation (Enums.AnimationState state) {
    Animator animator = gameObject.GetComponent<Animator>();
    if (state == Enums.AnimationState.Standing) animator.Play("Idle");
    else if (state == Enums.AnimationState.Moving) animator.Play("Walking");
    else if (state == Enums.AnimationState.Attacking) animator.Play("Attacking");
    else if (state == Enums.AnimationState.TakeDamage) animator.Play("TakeDamage");
    
  }
  
  public void GoTowards (Vector3 destination, float speed) {
    Face(destination);
    Vector3 direction = (destination - transform.position).normalized;
    transform.Translate(direction * speed, Space.World);
  }
  
  public void Face (Vector3 destination) {
    transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
  }

}

}
