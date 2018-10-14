using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Phase {

      public Shape[] shapesArray ;
      [NonSerialized]
	public List<Shape> shapes = new List<Shape>();
      public void AddShape(Shape p)
      {
            shapes.Add(p);
      }

      public void FinishShapeAdding(){
            shapesArray = shapes.ToArray();
            Debug.Log(shapesArray[0].posX);
      }

      public override string ToString()
      {
      string res = "";

      foreach(Shape s in shapes)
      {
            res += s.ToString();
            res += " \n";
      }

      return res;
      }
}
