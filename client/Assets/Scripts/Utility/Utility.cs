using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Utility {

  public static List<User> shuffleUsers (List<User> users, ref System.Random random) {
  	List<User> list = new List<User>();
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
  
}
