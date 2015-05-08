using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class Utility {

  public static List<User> ShuffleUsers (List<User> users, ref System.Random random) {
    var list = new List<User>();
    users.ForEach(list.Add);
    for (int i=0; i<list.Count; i++) {
      User temp = list[i];
      int randomIndex = random.Next(i, list.Count);
      list[i] = list[randomIndex];
      list[randomIndex] = temp;
    }
    return list;
  }

  public static T DeepClone<T>(T obj)
  {
    using (var ms = new MemoryStream())
    {
      var formatter = new BinaryFormatter();
      formatter.Serialize(ms, obj);
      ms.Position = 0;

      return (T) formatter.Deserialize(ms);
    }
  }
  
  public static void PositionGameObject (GameObject item, float x, float y) {
    RectTransform rt = item.GetComponent<RectTransform>();
    if (rt == null) return;
    Vector2 position = new Vector2(x,y);
    rt.anchoredPosition = position;
  }

}
