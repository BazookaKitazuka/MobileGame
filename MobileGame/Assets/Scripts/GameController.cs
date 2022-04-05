using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject NodePrehab;
    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = this.GetComponent<RectTransform>();
        createNodes();
    }


    void createNodes()
    {
        for (int i = 0; i<5; i++)
        {
            for (int j= 0; j< 5; j++) { 
                GameObject node = Instantiate(NodePrehab);
            node.transform.SetParent(this.transform);
                Vector2 pos = new Vector2(i* 200, j *-200);
                node.GetComponent<RectTransform>().anchoredPosition = pos;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
