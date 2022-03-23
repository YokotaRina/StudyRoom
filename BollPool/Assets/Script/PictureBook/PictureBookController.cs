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
        private void ListInitialize()
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
            foreach (var test in result.Rows)
            {
                // プレハブを生成
                var obj = Instantiate(_listContent);
                obj.transform.parent = _contentList.transform;
                obj.transform.localScale = Vector3.one;
                obj.Initialize(_menuObject, (int)test["id"], (string)test["name"], (id) =>
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
