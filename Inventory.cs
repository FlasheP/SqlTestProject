using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class Inventory : MonoBehaviour
{
    Action<string> _createItemsCallback;
    public GameObject ItemsUI;
    
    void Start()
    {
        _createItemsCallback = (jsonArrayString) => {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };
    }
    public void CreateItems()
    {
        ItemsUI.SetActive(true);
        string playerID = GameManager.Instance.playerInfo.PlayerID;
        StartCoroutine(GameManager.Instance.web.GetItemsIDs(playerID, _createItemsCallback)); //아이템 jsonArray 가져오기
    }
    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;
        for (int i = 0; i < jsonArray.Count; i++)
        {
            string itemID = jsonArray[i].AsObject["itemID"];
            JSONObject itemInfoJson = new JSONObject();

            Action<string> _getItemInfoCallback = (itemInfo) => {
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };
            yield return StartCoroutine(GameManager.Instance.web.GetItem(itemID, _getItemInfoCallback));//ID를 기반으로 아이템 정보 가져오기

            //아이템 프리펩 인스턴스화
            GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            item.transform.SetParent(ItemsUI.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
            //아이템 정보 출력
            item.transform.Find("Name").GetComponent<Text>().text = itemInfoJson["name"];
            item.transform.Find("Price").GetComponent<Text>().text = itemInfoJson["price"];
            item.transform.Find("Description").GetComponent<Text>().text = itemInfoJson["description"];

            int imgVer = itemInfoJson["imgVer"].AsInt;

            byte[] bytes = ImageManager.Instance.LoadImage(itemID, imgVer);

            if (bytes.Length == 0)
            {
                Action<byte[]> _getItemIconCallback = (downloadedBytes) =>{
                    Sprite sprite = ImageManager.Instance.BytesToSprite(downloadedBytes);
                    item.transform.Find("ItemIcon").GetComponent<Image>().sprite = sprite;
                    ImageManager.Instance.SaveImage(itemID, downloadedBytes, imgVer);
                    ImageManager.Instance.SaveVersionJson();
                };
                StartCoroutine(GameManager.Instance.web.GetItemIcon(itemID, _getItemIconCallback));
            }
            else
            {
                Sprite sprite = ImageManager.Instance.BytesToSprite(bytes);
                item.transform.Find("ItemIcon").GetComponent<Image>().sprite = sprite;
            }

            //아이템 판매 버튼 기능 넣기
            item.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() => {
                string iID = itemID;
                string uID = GameManager.Instance.playerInfo.PlayerID;
                StartCoroutine(GameManager.Instance.web.SellItem(iID,uID));
            });
        }
    }
}
