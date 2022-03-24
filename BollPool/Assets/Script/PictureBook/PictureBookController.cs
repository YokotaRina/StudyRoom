using System;
using SQLiteUnity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace Script.PictureBook
{
    /// <summary>
    /// PictureBookコントローラー
    /// </summary>
    public class PictureBookController : MonoBehaviour
    {
        [Header("List")]
        [SerializeField] private GameObject _listObject;
        [SerializeField] private Button _listReturnButton;
        [SerializeField] private GameObject _contentList;
        [SerializeField] private ListContentBehaviour _listContent;

        [Header("Menu")]
        [SerializeField] private GameObject _menuObject;
        [SerializeField] private Button _showDataButton;
        [SerializeField] private Button _deleteButton;

        [Header("Detail")]
        [SerializeField] private GameObject _detailObject;
        [SerializeField] private Image _image;
        [SerializeField] private Text _name;
        [SerializeField] private Text _number;
        [SerializeField] private Text _class;
        [SerializeField] private Text _type1;
        [SerializeField] private Text _type2;
        [SerializeField] private Text _description;
        [SerializeField] private Button _detailReturnButton;

        private int choiceId = 0;
        private const string NUMBER_TEXT_FORMAT = "№.{0:D3}";
        private const string CLASS_TEXT_FORMAT = "{0}ポケモン";

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize(MenuController menuController)
        {
            choiceId = 0;

            // 各種ボタン押下時の処理
            _listReturnButton.onClick.AddListener(() =>
            {
                menuController.gameObject.SetActive(true);
                _menuObject.SetActive(false);
                this.gameObject.SetActive(false);
            });
            _showDataButton.onClick.AddListener(() =>
            {
                _detailObject.SetActive(true);
                SQLite sqlite = new SQLite("test.db");
                string query = $"SELECT * FROM pokemon WHERE id = {choiceId}";
                var result = sqlite.ExecuteQuery(query);


                {
                    foreach (var rowData in result.Rows)
                    {
                        var id = $"{(int)rowData["id"]:D3}";
                        var path = $"SQLiteTest/Monster/{id}";
                        if (System.IO.File.Exists("Assets/Resources/"+path+".png"))
                        {
                            Sprite image = Resources.Load<Sprite>(path);
                            _image.sprite = image;
                        }
                        else
                        {
                            Sprite image = Resources.Load<Sprite>("SQLiteTest/Monster/000");
                            _image.sprite = image;
                        }

                        _name.text = (string)rowData["name"];
                        _number.text = string.Format(NUMBER_TEXT_FORMAT, id);
                        _class.text = string.Format(CLASS_TEXT_FORMAT, (string)rowData["class"]);
                        _type1.text = ((Type)Enum.ToObject(typeof(Type), (int)rowData["type1"])).ToDisplayName();
                        var type2RowData = (int)rowData["type2"];
                        if (type2RowData != 0) _type2.text = ((Type) Enum.ToObject(typeof(Type), type2RowData)).ToDisplayName();
                        else _type2.text = "なし";
                        _description.text = (string)rowData["detail"];
                    }
                }

                
                sqlite.Dispose();
            });
            _deleteButton.onClick.AddListener(() =>
            {
                SQLite sqlite = new SQLite("test.db");
                string query = $"DELETE FROM pokemon WHERE id = {choiceId}";
                sqlite.ExecuteNonQuery(query);
                sqlite.Dispose();
                ListInitialize();
            });
            _detailReturnButton.onClick.AddListener(() =>
            {
                _detailObject.SetActive(false);
            });

            // 図鑑表示
            ListInitialize();
        }

        /// <summary>
        /// リストの初期化
        /// </summary>
        public void ListInitialize()
        {
            // 一回全削除
            foreach (Transform child in _contentList.transform)
            {
                Destroy(child.gameObject);
            }

            // IDの重複確認し問題なければInsert
            SQLite sqlite = new SQLite("test.db");
            string query = $"SELECT * FROM pokemon";
            var result = sqlite.ExecuteQuery(query);
            foreach (var rowData in result.Rows)
            {
                // プレハブを生成
                var obj = Instantiate(_listContent);
                obj.transform.SetParent(_contentList.transform);
                obj.transform.localScale = Vector3.one;
                obj.Initialize(_menuObject, (int)rowData["id"], (string)rowData["name"], (id) =>
                {
                    choiceId = id;
                });
            }
            sqlite.Dispose();
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            foreach (Transform child in _contentList.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
