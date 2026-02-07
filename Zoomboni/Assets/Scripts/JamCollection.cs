using UnityEngine;

public class JamCollection : MonoBehaviour
{

    public string label = "DUMMY";
    

    void Update()
    {
        if (transform.childCount == 0)
        {
            LevelManager.GetInstance().SetAllCleaned(this);
            Destroy(this.gameObject);
        }
    }

}
