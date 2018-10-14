using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PhaseJson {

      public ShapeJson[] shapesArray ;
      [NonSerialized]
	public List<ShapeJson> shapes = new List<ShapeJson>();
      public void AddShape(ShapeJson p)
      {
            shapes.Add(p);
      }

      public void FinishShapeAdding(){
            shapesArray = shapes.ToArray();
      }

      public override string ToString()
      {
      string res = "";

      foreach(ShapeJson s in shapes)
      {
            res += s.ToString();
            res += " \n";
      }

      return res;
      }
}
