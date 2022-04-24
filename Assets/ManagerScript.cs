using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public GameObject camera;
    public List<Material> materials = new List<Material>();
    private int currentObject = 0;
    public List<string> walls = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
        walls.Add("wallTag0");
        walls.Add("wallTag1");
        //Debug.Log(GameObject.FindGameObjectsWithTag(walls[0]).Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r")){
            currentObject = (currentObject + 1) % objects.Count;
        }
        if(Input.GetKeyDown("c")){
            foreach(GameObject wall in GameObject.FindGameObjectsWithTag(walls[0])){
                Renderer r = wall.GetComponent<Renderer>();
                Material m = r.material;
                Color c = m.color;
                m.color = new Color(c.r, c.g, c.b, 0.3f);
            }
            /*
            Debug.Log(walls[0]);
            List<GameObject> wall0s = FindAllPrefabInstances(walls[0]);
            Renderer renderer = wall0s[0].GetComponent<Renderer>();
            Material mat = renderer.material;
            Color past_color = mat.color;
            mat.color = new Color(past_color.r, past_color.g, past_color.b, 0.3f);
            //Color past_color = materials[0].color;
            //materials[0].color = new Color(past_color.r, past_color.g, past_color.b, 0.3f);*/
        }

        Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
        if (Input.GetKey("w")) {
            direction = new Vector3(0.0f, 0.0f, 1.0f);
        }
        if (Input.GetKey("s")) {
            direction = new Vector3(0.0f, 0.0f, -1.0f);
        }
        if (Input.GetKey("d")) {
            direction = new Vector3(1.0f, 0.0f, 0.0f);
        }
        if (Input.GetKey("a")) {
            direction = new Vector3(-1.0f, 0.0f, 0.0f);
        }
        objects[currentObject].GetComponent<cube_script>().UpdatePosition(direction);
        camera.GetComponent<CameraScript>().UpdatePosition(objects[currentObject].transform.position);
    }
}
