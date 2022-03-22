using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.PictureBook
{
    /// <summary>
    /// Menuコントローラー
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] private Button _insertButton;
        [SerializeField] private Button _pictureBookButton;

        [Header("Controller")]
        [SerializeField] private InsertController _insertController;
        [SerializeField] private PictureBookController _pictureBookController;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            
        }
    }
}
