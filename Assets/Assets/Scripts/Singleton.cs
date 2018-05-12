using UnityEngine;
using System.Collections;
/*****************************************************************************************************/
// The Singleton class is responsible for ensuring the background music plays across different scenes 
/*****************************************************************************************************/	
public class Singleton : MonoBehaviour {
   
   private static Singleton instance = null;
   
   public static Singleton Instance {
	   get { return instance; }
   }
/***************************************************************************************************/	
// Awake is called when the script is being loaded
/***************************************************************************************************/		
   
   void Awake() {
      if (instance != null && instance != this) {
		  Destroy(this.gameObject);
   	     return;
      } 
	  else 
	  {
   	    instance = this;
      }
   	  DontDestroyOnLoad(this.gameObject);
   }
}
/***************************************************************************************************/
//
// 	End of Class
//	
/***************************************************************************************************/