using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    
    [SerializeField] private float hp;
    private TextMesh t;

	// Use this for initialization
	void Start () {
        
        GameObject text = new GameObject();
        t = text.AddComponent<TextMesh>();
        
        t.fontSize = 20;

        //t.transform.localEulerAngles += new Vector3(90, 0, 0);
        t.transform.position = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        t.text = "" + hp;
        if (hp <= 0)
        {
            Destroy(gameObject);
            Destroy(t.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Debug.Log("vida " + hp);
            hp -= PlayerStats.GetMagicDamage();
        }

        if (collision.gameObject.tag == "punch")
        {
            hp -= PlayerStats.GetDamage();
        }
    }

}
