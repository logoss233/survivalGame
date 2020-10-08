using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Grid : MonoBehaviour
{
    
    bool dying=false;
    float dieTimer = 1.4f;
    public MeshRenderer meshRenderer;
    public Transform trans;
    public Animator ani;
    public int floor = 0; //属于第几层

    Material material;

    public bool isStartGrid = false; //开始用的板子，不会自动消失

    private void Awake()
    {
        material = new Material(meshRenderer.material);
        meshRenderer.material = material;
        
    }
    private void Start()
    {
        if (Random.value < 0.7)
        {
            meshRenderer.material.color = Game.inst.colors[floor * 2];
        }
        else
        {
            meshRenderer.material.color = Game.inst.colors[floor * 2 + 1];
        }
        if (floor == 5)
        {
            isStartGrid = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (dying)
        {

            dieTimer -= Time.deltaTime;
            if (dieTimer <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isStartGrid && !dying && collision.collider.tag == "Character")
        {
            dying = true;
            ani.Play("Shake", 0, 0);
            StartCoroutine(dieAni());
        }
    }

    IEnumerator dieAni()
    {
        yield return new WaitForSeconds(0.5f);
        Color c = Color.black;
        bool finish = false;
        DOTween.To(() => { return c; }, x => { c = x; }, Color.white, 0.8f).OnComplete(()=> { finish = true; });
        while (!finish)
        {
            meshRenderer.material.SetColor("_EmissionColor", c);
            yield return 0;
        }
    }

    /// <summary>
    /// 手动调用死亡，在isStartGrid=true时用
    /// </summary>
    public void manualDie()
    {
        dying = true;
        StartCoroutine(dieAni());
    }
}
