using UnityEngine;

public class BGloop : MonoBehaviour
{
    public GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenbounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       mainCamera = gameObject.GetComponent<Camera>();
        screenbounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        foreach (GameObject obj in levels)
        {
            loadChildObjects(obj);
        }




    }

    
    void loadChildObjects(GameObject obj)
    {
        float objectwith = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childsNeeded = (int)Mathf.Ceil(screenbounds.x * 2 / objectwith);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectwith * i, obj.transform.position.y, obj.transform.position.z);
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());


    }
}
