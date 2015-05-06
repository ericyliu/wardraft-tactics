using System.Collections.Generic;

namespace Wardraft.Game {

  public class Terrain {
  
    public int code;
    public FInt speedCost = FInt.Create(2.5);
    public List<Aura> auras;
    public bool passable = true;
  
  }

}
