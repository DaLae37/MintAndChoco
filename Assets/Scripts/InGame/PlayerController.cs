using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public bool isLocal = false, isInput = false;

    public float moveSpeed, rotateSpeed;

    Vector3 oldPos;

    //받는 값
    public Vector3 currentPos, currentVel;

    Quaternion oldRot;

    int hp;

    //받는 값
    public Quaternion currentRot;

    public GameObject bulletPrefab;

    float h, v;

    [SerializeField]
    float syncTime;

    //캐싱
    Transform tr;
    Rigidbody ri;

    public Transform camPos;
    public void SetLocal(bool _isLocal)
    {
        isLocal = _isLocal;
        ri.useGravity = true;
        if (_isLocal)
            cameraRot.instance.SetTarget(camPos);
    }

    public void SetInput(bool _isInput)
    {
        isInput = _isInput;
    }


    private void Awake()
    {
        instance = this;
        tr = GetComponent<Transform>();
        oldRot = tr.rotation;
        oldPos = tr.position;
        ri = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isLocal && isInput)
        {
            
            //에디터 전용 움직임
            h = joyStick.instance.Horizontal();
            v = joyStick.instance.Vertical();

        }
        else
        {
            syncTime += Time.deltaTime;
            if (currentVel.normalized != Vector3.zero)
            {
                // 달리는 애니메이션 실행
            }
            else
            {
                //대기 상태 애니메이션 실행
            }

        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (isLocal)
        {
            Vector3 movePos = ((tr.forward * v * moveSpeed) + (tr.right * h * moveSpeed));
            movePos.y = ri.velocity.y;

            //에디터 전용 움직임
            ri.velocity = movePos;

            SendPosition();
            SendRotation();
        }
        else
        {
            //현재 위치 = 이전 위치 + ( 속도 * 시간 ) + ( 1 / 2 * 가속도 * 시간 ^ 2 )
            tr.position = currentPos + ((currentVel * syncTime) + (0.5f * currentVel * syncTime * syncTime));
        }
    }

    public void SendHp()
    {
        NetworkManager.instance.EmitHp(hp);
    }

    public void SendPosition()
    {
        if (oldPos != tr.position)
        {
            NetworkManager.instance.EmitMove(tr.position, ri.velocity);
            oldPos = tr.position;
        }
    }

    public void SendRotation()
    {
        if (oldRot.x != tr.rotation.x)
        { 
            oldRot = tr.rotation;
            NetworkManager.instance.EmitRotate(tr.rotation);
        }
    }

    public void SetPosition(Vector3 _currentPos, Vector3 _currentVel)
    {
        syncTime = 0;
        currentPos = _currentPos;
        currentVel = _currentVel;
    }

    public void SetRotation(Quaternion _rot)
    {
        tr.rotation = _rot;
    }

    public void SetHp(int _hp)
    {
        hp = _hp;
    }
    /// <summary>
    /// 서버에서 발사를 받아서 쏩니다.
    /// </summary>
    public void ShotBullet()
    {
        Instantiate(bulletPrefab, tr.position, tr.rotation);
    }

    public void SendBulllet()
    {
        NetworkManager.instance.EmitBullet(0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            hp -= Bullet.damage;
            SendHp();
        }
    }
}
