using System;
using UnityEngine;
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
