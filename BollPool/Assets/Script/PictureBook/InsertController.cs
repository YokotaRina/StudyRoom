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
