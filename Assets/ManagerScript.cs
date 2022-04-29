using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public GameObject camera;
    public List<GameObject> wallPrefabs = new List<GameObject>();
    private int currentObject = 0;
    private int numTypes = 2;
    private List<string> wallTags;
    private List<string> buttonTags;
    private List<bool> activated;
    
    // Start is called before the first frame update
    void Start()
    {
        wallTags = new List<string>();
        wallTags.Add("wallTag0");
        wallTags.Add("wallTag1");

        buttonTags = new List<string>();
        buttonTags.Add("ButtonTag0");
        buttonTags.Add("ButtonTag1");

        activated = new List<bool>();
        for(int i=0; i<numTypes; i++){
            activated.Add(false);
        }
        //Debug.Log(GameObject.FindGameObjectsWithTag(walls[0]).Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r")){
            currentObject = (currentObject + 1) % players.Count;
        }
        /*
        if(Input.GetKeyDown("c")){
            foreach(GameObject wall in GameObject.FindGameObjectsWithTag(walls[0])){
                Renderer r = wall.GetComponent<Renderer>();
                Material m = r.material;
                Color c = m.color;
                m.color = new Color(c.r, c.g, c.b, 0.3f);
            }
        }*/
        for(int i=0; i<numTypes; i++){
            activated[i] = false;
            foreach(GameObject button in GameObject.FindGameObjectsWithTag(buttonTags[i])){
                foreach(GameObject player in players){
                    if(button.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds)){
                        activated[i] = true;
                    }
                }
            }
            
            if(activated[i]){
                Material m = wallPrefabs[i].GetComponent<Renderer>().sharedMaterial;
                Color c = m.color;
                m.color = new Color(c.r, c.g, c.b, 0.1f);
                foreach(GameObject wall in GameObject.FindGameObjectsWithTag(wallTags[i])){
                    wall.GetComponent<Collider>().isTrigger = true;
                }
            }
            else{
                Material m = wallPrefabs[i].GetComponent<Renderer>().sharedMaterial;
                Color c = m.color;
                m.color = new Color(c.r, c.g, c.b, 1f);
                foreach(GameObject wall in GameObject.FindGameObjectsWithTag(wallTags[i])){
                    wall.GetComponent<Collider>().isTrigger = false;
                }
            }
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
        players[currentObject].GetComponent<cube_script>().UpdatePosition(direction);
        camera.GetComponent<CameraScript>().UpdatePosition(players[currentObject].transform.position);
    }
}
