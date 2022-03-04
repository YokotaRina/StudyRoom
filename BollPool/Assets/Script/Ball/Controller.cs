using System.Collections.Generic;
using Script.State;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Script.Ball
{
    /// <summary>
    /// コントローラー
    /// </summary>
    public class Controller : MonoBehaviour
    {
        [SerializeField] private DestroyMaterial _ball;
        [Header("Button")]
        [SerializeField] private Button _popButton;
        [SerializeField] private Button _autoPopButton;
        [SerializeField] private Button _stopAutoPopButton;
        [SerializeField] private Button _deleteButton;

        private List<DestroyMaterial> _destroyMaterialList;
        private List<Color> _colorList;

        private int maxCount = 100;
        private float time = 0;

        /// <summary>
        /// ステートの種類
        /// </summary>
        private enum State
        {
            SelectWait,  // 入力待ち
            Pop,         // 個別Pop
            AutoPop,     // 自動Popの開始
            StopAutoPop, // 自動Popの停止
            Delete,      // Ballの削除
        }

        /// <summary>
        /// ステート
        /// </summary>
        private StateManager _state;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            _state = new StateManager();

            _destroyMaterialList = new List<DestroyMaterial>();
            _colorList = new List<Color>
            {
                new Color(0, 1, 1, 0.5f),
                new Color(0, 1, 0, 0.5f),
                new Color(1, 0, 1, 0.5f),
                new Color(1, 1, 0, 0.5f),
            };

            // 各種ボタン押下時の処理
            _popButton.onClick.AddListener(() => _state.SetState((int)State.Pop));
            _autoPopButton.onClick.AddListener(() => _state.SetState((int)State.AutoPop));
            _stopAutoPopButton.onClick.AddListener(() => _state.SetState((int)State.StopAutoPop));
            _deleteButton.onClick.AddListener(() => _state.SetState((int)State.Delete));
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            switch ((State)_state.GetState())
            {
                case State.SelectWait:
                    // 何もしない
                    break;
                case State.Pop:
                    if (_state.IsOnEntry())
                    {
                        this.CreateBall();
                        _state.SetState((int)State.SelectWait);
                    }
                    break;
                case State.AutoPop:
                    // ボタンの表示変更
                    _popButton.gameObject.SetActive(false);
                    _autoPopButton.gameObject.SetActive(false);
                    _stopAutoPopButton.gameObject.SetActive(true);
                    _deleteButton.gameObject.SetActive(false);

                    // 1秒ごとにPopを増やす
                    // ※最大100個まで
                    time += Time.deltaTime;
                    if (time > 1)
                    {
                        time = 0.0f;
                        if (_destroyMaterialList.Count < 100) this.CreateBall();
                    }
                    break;
                case State.StopAutoPop:
                    if (_state.IsOnEntry())
                    {
                        // ボタンの表示変更
                        _popButton.gameObject.SetActive(true);
                        _autoPopButton.gameObject.SetActive(true);
                        _stopAutoPopButton.gameObject.SetActive(false);
                        _deleteButton.gameObject.SetActive(true);

                        _state.SetState((int)State.SelectWait);
                    }
                    break;
                case State.Delete:
                    if (_state.IsOnEntry())
                    {
                        foreach (var destroyMaterial in _destroyMaterialList)
                        {
                            Destroy(destroyMaterial.gameObject);
                        }

                        _destroyMaterialList.Clear();

                        _state.SetState((int)State.SelectWait);
                    }
                    break;
                    
            }
            _state.Update(Time.unscaledDeltaTime);
            return;



           
        }

        /// <summary>
        /// Ballの生成
        /// </summary>
        private void CreateBall()
        {
            // プレハブの位置をランダムで設定
            float x = Random.Range(-5.0f, 5.0f);
            float z = Random.Range(-5.0f, 5.0f);
            Vector3 pos = new Vector3(x, 10.0f, z);

            // 色をランダムで設定
            int color = Random.Range(0, 4);

            // プレハブを生成
            var obj = Instantiate(_ball, pos, Quaternion.identity);
            var rend = obj.GetComponent<Renderer>();
            rend.material.color = _colorList[color];

            _destroyMaterialList.Add(obj);
        }
    }
}
