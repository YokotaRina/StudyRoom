using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// コントローラー
/// </summary>
public class Controller : MonoBehaviour
{
    [SerializeField] private DestroyMaterial _pop;
    [SerializeField] private Button _deleteButton;

    private List<DestroyMaterial> _destroyMaterialList;
    private List<Color> _colorList;

    private int maxCount = 100;
    private float time = 0;

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _destroyMaterialList = new List<DestroyMaterial>();
        _colorList = new List<Color>
        {
            new Color(0, 1, 1, 0.5f),
            new Color(0, 1, 0, 0.5f),
            new Color(1, 0, 1, 0.5f),
            new Color(1, 1, 0, 0.5f),
        };

        // Deleteボタン押下時の処理
        _deleteButton.onClick.AddListener(() =>
        {
            foreach (var destroyMaterial in _destroyMaterialList)
            {
                Destroy(destroyMaterial.gameObject);
            }

            _destroyMaterialList.Clear();
        });
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        // 1秒ごとにPopを増やす
        // ※最大100個まで
        time += Time.deltaTime;
        if (time > 1)
        {
            time = 0.0f;
            if (_destroyMaterialList.Count < 100)
            {
                // プレハブの位置をランダムで設定
                float x = Random.Range(-5.0f, 5.0f);
                float z = Random.Range(-5.0f, 5.0f);
                Vector3 pos = new Vector3(x, 10.0f, z);

                // 色をランダムで設定
                int color = Random.Range(0, 4);

                // プレハブを生成
                var obj = Instantiate(_pop, pos, Quaternion.identity);
                var rend = obj.GetComponent<Renderer>();
                rend.material.color = _colorList[color]; // ここでリークする

                _destroyMaterialList.Add(obj);
            }
        }
    }
}
