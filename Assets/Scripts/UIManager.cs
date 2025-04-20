using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
   public void LoadLevel()
   {
      SceneManager.LoadScene("Level_1");
   }
}
