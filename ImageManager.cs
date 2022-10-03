using UnityEngine;
using System.IO;
using SimpleJSON;

public class ImageManager : MonoBehaviour
{
    private static ImageManager instance = null;

    string _basePath;
    string _versionJsonPath;
    JSONObject _versionJson;
    public static ImageManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            else
                return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        _basePath = Application.persistentDataPath + "/Images/";
        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
        _versionJson = new JSONObject();
        _versionJsonPath = _basePath + "VersionJson";
        if (File.Exists(_versionJsonPath))
        {
            string jsonString = File.ReadAllText(_versionJsonPath);
            _versionJson = JSON.Parse(jsonString) as JSONObject;
        }
    }
    bool IsExistingImage(string name)
    {
        return File.Exists(_basePath + name);
    }
    public void SaveImage(string name, byte[] bytes, int imgVer)
    {
        File.WriteAllBytes(_basePath + name, bytes);
        UpdateVersionJson(name, imgVer);
    }
    public byte[] LoadImage(string name, int imgVer)
    {
        byte[] bytes = new byte[0];

        //없는 파일이거나 버전이 낮으면 빈 배열을 반환한다
        if (!IsImageUpToDate(name, imgVer))
            return bytes;

        if (IsExistingImage(name))
            bytes = File.ReadAllBytes(_basePath + name);

        return bytes;
    }
    public Sprite BytesToSprite(byte[] bytes)
    {
        //텍스처 만들기
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);
        //스프라이트 만들고 텍스처 먹이기
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        return sprite;
    }
    void UpdateVersionJson(string name, int ver)
    {
        _versionJson[name] = ver;
    }
    bool IsImageUpToDate(string name, int ver)
    {
        if(_versionJson[name] != null)
            return _versionJson[name].AsInt >= ver;
        return false;
    }
    public void SaveVersionJson()
    {
        File.WriteAllText(_versionJsonPath, _versionJson.ToString());
    }

}
