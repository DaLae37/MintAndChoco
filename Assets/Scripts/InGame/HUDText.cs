using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDText : MonoBehaviour
{
    Transform mouseName;
    Transform catName;
    RectTransform rt;
    Transform HUDTransform;
    Text tx;
    
    private void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        tx = gameObject.GetComponent<Text>();
    }
    private void Update()
    {
        if (HUDTransform != null)
            rt.position = Camera.main.WorldToScreenPoint(HUDTransform.position);
        else if(GameObject.FindWithTag("MouseNamePos") != null && GameObject.FindWithTag("CatNamePos") != null)
            setTransform();
    }
    void setTransform()
    {
        mouseName = GameObject.FindWithTag("MouseNamePos").transform;
        catName = GameObject.FindWithTag("CatNamePos").transform;
        if (PlayerDataManager.instance.my.isCat)
        {
            HUDTransform = mouseName;
            tx.text = GameObject.FindWithTag("Mouse").gameObject.name;
        }
        else
        {
            HUDTransform = catName;
            tx.text = GameObject.FindWithTag("Cat").gameObject.name;
        }
    }
}
