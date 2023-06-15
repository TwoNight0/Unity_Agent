using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour{
    #region ������Ʈ
    public Transform target;
    private Vector3 vector_target;
    [SerializeField] private Image cur_HpImg;
    public Renderer objectColor;
    public RectTransform uiRectHP;
    #endregion

    #region ����
    public float cur_Hp;
    public float max_Hp;
    public float dmg_physical;
    public float dmg_magical;
    private float Speed;
    #endregion

    [SerializeField] private bool attackMode;//���� ���� ���


    private void Start(){
        max_Hp = 50;
        cur_Hp = max_Hp;
        dmg_magical = 10.0f;
        dmg_physical = 10.0f;
        Speed = 1.0f;
        objectColor = gameObject.GetComponent<Renderer>();
        target = PlayerMng.Instance.transform;
        attackMode = false;
        gameObject.AddComponent<BoxCollider>();
    }

    private void Update(){
        death();
        LookAtTarget();
        hpBarApply();
        MonsterMove();
    }


    private void createitem(){

    }

    //ü�¹� ����
    public void hpBarApply(){
        cur_HpImg.fillAmount = cur_Hp / max_Hp;
    }

    private void death(){
        // �����̹���.fillAmount = ��°� / �ִ�ġ ;
       
        if (cur_Hp <= 0){
            transform.gameObject.SetActive(false);
        }
    }

    public void changeColor(Color color){
        objectColor.material.color = color;
    }

    public void LookAtTarget(){      
        Vector3 testPos = new Vector3(target.position.x, uiRectHP.position.y, target.position.z);
        testPos.y = 3; //�������� �����ϼ̴� ���� �̰Ű���
        //Debug.Log(uiRectHP.position.y);
        //Debug.Log(testPos);

        //2. �迧�Ҷ� y��ǥ�� ����� �ٲٱ� �ȵ�.
        
        uiRectHP.LookAt(testPos);


        //uiRectHP.LookAt(target.position);
        

        //1. Ư�������� ȸ���ϱ� 
        //uiRectHP.rotation = Quaternion.Euler(0, 200, 0);
    }

    private void MonsterMove(){
        if (attackMode){
            vector_target = target.position;
            //transform.Translate(-vector_target * Time.deltaTime);
            transform.Translate((target.position - transform.position) * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);

        }
    }
    private void OnCollisionEnter(Collision collision){
        if (collision.transform.tag == "Player"){
            Debug.Log("�÷��̾�� �浹��");
            PlayerMng.Instance.TakeDmg(dmg_physical, dmg_physical);


        }
    }
}