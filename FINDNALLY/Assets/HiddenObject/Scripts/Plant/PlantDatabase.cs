using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlantDatabase : ScriptableObject
{
   public Plant[] plant;

   public int PlantCount
   {
      get
      {
         return plant.Length;
      }
   }

   public Plant GetPlant(int index)
   {
      return plant[index];
   }
}
