using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
   [SerializeField] public static Camera mainCamera;

   [SerializeField] public static LayerMask placementLayerMask;

   private static Vector3  lastPosition;
   
   static float halfSize = 0.95f;

   public static Vector3 GetMousePosition()
   {
      Vector3 mousePosition = Input.mousePosition;
      mousePosition.z = mainCamera.nearClipPlane;
      Ray ray = mainCamera.ScreenPointToRay(mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
      {
          lastPosition = hit.point;
          lastPosition.x = Mathf.Floor(lastPosition.x / halfSize) * halfSize;
          lastPosition.z = Mathf.Floor((lastPosition.z / halfSize)) * halfSize;

      }

      return lastPosition;
   }
}
