using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.SceneManagement;

/// <summary>
/// 네트워크를 관리하는 유일한 매니저 입니다.
/// </summary>
public class NetworkManager : MonoBehaviour
{

    public static NetworkManager instance;

    public List<User> userList;

    public PlayerController FindUserController(string _name)
    {
        for (int i = 0; i < userList.Count; i++)
        {
            if (userList[i].name == _name)
                return userList[i].controller;
        }

        return null;
    }

    public void SetAllUserInput(bool _isInput)
    {
        for (int i = 0; i < userList.Count; i++)
        {
            userList[i].controller.SetInput(_isInput);
        }
    }

    SocketIOComponent socket;

    private void Awake()
    {
        socket = GetComponent<SocketIOComponent>();

        if (instance != null)
            Destroy(this.gameObject);

        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    // Socket 에서 들어오는 데이터 값들은 항상 여기서 미리 선언 해야해요.
    void Start()
    {
        socket.On("join", OnJoin);
        socket.On("pick", OnMatch);
        socket.On("gameLoad", OnGameLoad);
        socket.On("move", OnMove);
        socket.On("bullet", OnBullet);
        socket.On("userData", OnUserData);

        StartCoroutine(TestConnect());
    }

    IEnumerator TestConnect()
    {
        yield return new WaitForSeconds(1f);
        EmitJoin("User"+Random.Range(0,1000));
    }

    #region JoinMethod

    public void OnJoin(SocketIOEvent e)
    {
        JSONObject json = e.data;
        PlayerDataManager.instance.my = new User(json.GetField("name").str);
    }

    /// <summary>
    /// 닉네임 정보를 보냅니다.
    /// </summary>
    /// <param name="name"></param>
    public void EmitJoin(string _name)
    {
        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("name", _name);

        socket.Emit("join", json);
    }

    #endregion

    #region PickMethod

    //public void OnPick(SocketIOEvent e)
    //{
    //    JSONObject json = e.data;


    //}

    ///// <summary>
    ///// 유저의 캐릭터 상태를 보냅니다.
    ///// </summary>
    ///// <param name="isCat"></param>
    //public void EmitPick(bool _isCat)
    //{
    //    JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
    //    json.AddField("name", _isCat);

    //    socket.Emit("join", json);
    //}

    #endregion

    #region MatchMethod

    /// <summary>
    /// 매칭 완료를 받습니다.
    /// </summary>
    /// <param name="e"></param>
    public void OnMatch(SocketIOEvent e)
    {
        //JSONObject json = e.data;
        SceneManager.LoadScene("InGame");
    }

    /// <summary>
    /// 매칭을 요청합니다.
    /// </summary>
    public void EmitMatch()
    {
        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("name", PlayerDataManager.instance.my.name);
        json.AddField("pick", PlayerDataManager.instance.my.isCat);

        socket.Emit("pick", json);
    }

    #endregion

    #region GameLoadMethod

    /// <summary>
    /// 게임 로드 완료를 받습니다.
    /// </summary>
    /// <param name="e"></param>
    public void OnGameLoad(SocketIOEvent e)
    {
        JSONObject json = e.data;

        float readyCount = json.GetField("ready").f;

        if (readyCount >= 2)
            EmitUserData();


    }

    /// <summary>
    /// 게임 로드의 완료를 보냅니다.
    /// </summary>
    public void EmitGameLoad()
    {
        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("ready", true);

        socket.Emit("gameLoad", json);
    }

    #endregion

    #region UserDataMethod

    public void OnUserData(SocketIOEvent e)
    {
        JSONObject json = e.data;
        string name = json.GetField("name").str;
        bool isCat = json.GetField("pick").b;

        if (name == PlayerDataManager.instance.my.name)
            userList.Add(PlayerDataManager.instance.my);
        else
            userList.Add(new User(name, isCat));

        if (userList.Count >= 2)
            GameManager.instance.StartGame();
    }

    /// <summary>
    /// 유저 데이터를 보냅니다.
    /// </summary>
    public void EmitUserData()
    {
        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("name", PlayerDataManager.instance.my.name);
        json.AddField("pick", PlayerDataManager.instance.my.isCat);

        socket.Emit("userData", json);
    }

    #endregion

    #region PositionMethod

    /// <summary>
    /// 위치를 받습니다.
    /// </summary>
    /// <param name="e"></param>
    public void OnMove(SocketIOEvent e)
    {
        JSONObject json = e.data;
        string name = json.GetField("name").str;
        Vector3 pos = new Vector3(json.GetField("posX").f, json.GetField("posY").f, json.GetField("posZ").f);
        Vector3 vel = new Vector3(json.GetField("velX").f, json.GetField("velY").f, json.GetField("velZ").f);

        FindUserController(name).SetPosition(pos, vel);
    }

    /// <summary>
    /// 위치를 보냅니다.
    /// </summary>
    public void EmitMove(Vector3 _pos, Vector3 _vel)
    {
        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("name", PlayerDataManager.instance.my.name);
        json.AddField("posX", _pos.x);
        json.AddField("posY", _pos.y);
        json.AddField("posZ", _pos.z);
        json.AddField("velX", _vel.x);
        json.AddField("velY", _vel.y);
        json.AddField("velZ", _vel.z);

        socket.Emit("move", json);
    }

    #endregion

    #region RotationMethod

    /// <summary>
    /// 위치를 받습니다.
    /// </summary>
    /// <param name="e"></param>
    public void OnRotate(SocketIOEvent e)
    {
        JSONObject json = e.data;
        string name = json.GetField("name").str;
        Quaternion rot = new Quaternion(json.GetField("rotX").f, json.GetField("rotY").f, json.GetField("rotZ").f, json.GetField("rotW").f);

        FindUserController(name).SetRotation(rot);
    }

    /// <summary>
    /// 위치를 보냅니다.
    /// </summary>
    public void EmitRotate(Quaternion _rot)
    {
        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("name", PlayerDataManager.instance.my.name);
        json.AddField("rotX", _rot.x);
        json.AddField("rotY", _rot.y);
        json.AddField("rotZ", _rot.z);
        json.AddField("rotW", _rot.w);

        socket.Emit("rotate", json);
    }

    #endregion

    #region BulletMethod

    public void OnBullet(SocketIOEvent e)
    {
        JSONObject json = e.data;
        string name = json.GetField("name").str;
        int bulletID = (int)json.GetField("bullet").f;

        FindUserController(name).ShotBullet();
    }

    /// <summary>
    /// 총 발사를 보냅니다.
    /// </summary>
    /// <param name="_bulletID"></param>
    public void EmitBullet(int _bulletID)
    {
        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("name", PlayerDataManager.instance.my.name);
        json.AddField("bullet", _bulletID);

        socket.Emit("bullet", json);
    }

    #endregion


}
