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
            _insertController.Initialize(this);
            _pictureBookController.Initialize(this);

            // 各種ボタン押下時の処理
            _insertButton.onClick.AddListener(() =>
            {
                _insertController.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            });
            _pictureBookButton.onClick.AddListener(() =>
            {
                _pictureBookController.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            });
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            Destroy(_insertController);
            Destroy(_pictureBookController);
        }
    }
}
