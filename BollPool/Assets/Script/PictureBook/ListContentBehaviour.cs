using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Script.PictureBook
{
    /// <summary>
    /// リストの中身のBehaviour
    /// </summary>
    public class ListContentBehaviour : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;
        private const string STRAIGHT_TEXT_FORMAT = "{0:D3} {1}";

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize(GameObject menuObject, int id, string name, UnityAction<int> clickEvent)
        {
            _text.text = string.Format(STRAIGHT_TEXT_FORMAT, id, name);

            _button.onClick.AddListener(() =>
            {
                menuObject.SetActive(true);
                clickEvent.Invoke(id);
            });
        }
    }
}
