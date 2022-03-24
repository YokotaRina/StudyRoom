using System;
using System.Collections.Generic;
using SQLiteUnity;
using UnityEngine;
using UnityEngine.UI;

namespace Script.PictureBook
{
    /// <summary>
    /// Insertコントローラー
    /// </summary>
    public class InsertController : MonoBehaviour
    {
        [Header("InputField")]
        [SerializeField] private InputField _name;
        [SerializeField] private InputField _number;
        [SerializeField] private InputField _class;
        [SerializeField] private InputField _description;

        [Header("DropDown")]
        [SerializeField] private Dropdown _type1;
        [SerializeField] private Dropdown _type2;

        [Header("Button")]
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _registorButton;

        [Header("Text")]
        [SerializeField] private Text _infoText;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize(MenuController menuController)
        {
            this.Clean();

            // 各種ボタン押下時の処理
            _returnButton.onClick.AddListener(() =>
            {
                menuController.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            });
            _deleteButton.onClick.AddListener(this.Clean);
            _registorButton.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(_number.text) || string.IsNullOrEmpty(_name.text) ||
                    string.IsNullOrEmpty(_class.text) || string.IsNullOrEmpty(_description.text) ||
                    _type1.value == (int)Type.None)
                {
                    _infoText.text = "みにゅうりょくこうもくがあります。";
                    return;
                }

                var number = int.Parse(_number.text);
                var name = _name.text;
                var className = _class.text;
                var description = _description.text;
                var type1 = _type1.value;
                var type2 = _type2.value;

                SQLite sqlite = new SQLite("test.db");

                // IDの重複確認し問題なければInsert
                string query = $"SELECT * FROM pokemon WHERE id = {number} LIMIT 1";
                var result = sqlite.ExecuteQuery(query);
                if (result.Rows.Count == 0)
                {
                    query = $"INSERT INTO pokemon VALUES ({number}, '{name}', '{className}', {type1}, {type2}, '{description}')";
                    sqlite.ExecuteNonQuery(query);
                    _infoText.text = "ずかんにとうろくしました。";
                    sqlite.Dispose();
                }
                else
                {
                    _infoText.text = "すでにとうろくずみのばんごうです。";
                }
            });

            // Dropdownの項目設定
            {
                List<string> optionList = new List<string>();
                foreach (Type value in Enum.GetValues(typeof(Type)))
                {
                    optionList.Add(value.ToDisplayName());
                }

                // 一度すべてのOptionsをクリア
                _type1.ClearOptions();
                _type2.ClearOptions();

                // リストを追加
                _type1.AddOptions(optionList);
                _type2.AddOptions(optionList);
            }
        }

        /// <summary>
        /// 全消し
        /// </summary>
        public void Clean()
        {
            _name.text = "";
            _number.text = "";
            _class.text = "";
            _description.text = "";
            _type1.value = (int)Type.None;
            _type2.value = (int)Type.None;
            _infoText.text = "";
        }
        
        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            
        }
    }
}
