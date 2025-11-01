using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace b0bmat0ca.Utility.Shader
{
    /// <summary>
    /// 指定された「クリッピングオブジェクト」と重なる部分をクリッピングするためのスクリプト
    /// </summary>
    public class ClippingController : MonoBehaviour
    {
        [SerializeField] private GameObject _targetModel;
        [SerializeField] private Transform _clippingObject;
        
        private Renderer[] _targetRenderers;
        private readonly string THRESHOLD_PROPERTY = "_Threshold";
        private readonly string WORLD_TO_CLIP_MATRIX_PROPERTY = "_WorldToClipMatrix";

        private List<Material> _allMaterials = new List<Material>();
        private Matrix4x4 _worldToClipMatrix;
        
        private bool _isClipEnabled = true;

        private void Awake()
        {
            Assert.IsNotNull(_targetModel, "Target model is not assigned.");
            Assert.IsNotNull(_clippingObject, "Clipping object is not assigned.");
        }
        
        private void Start()
        {
            SetAllMaterials();
        }
        
        private void Update()
        {
            if (!_isClipEnabled) return;
            
            // ワールド座標からクリッピングオブジェクトのローカル座標へと変換する行列を取得
            _worldToClipMatrix = _clippingObject.worldToLocalMatrix;

            // マテリアルに対して行列を設定する
            foreach (Material material in _allMaterials)
            {
                material.SetMatrix(WORLD_TO_CLIP_MATRIX_PROPERTY, _worldToClipMatrix);
            }
        }

        private void SetAllMaterials()
        {
            Material[] materials;
            _targetRenderers = _targetModel.GetComponentsInChildren<Renderer>();
            foreach (Renderer targetRenderer in _targetRenderers)
            {
                // 各マテリアルのクリッピング閾値を設定し、リストに追加する
                materials = targetRenderer.materials;
                foreach (Material material in materials)
                {
                    material.SetFloat(THRESHOLD_PROPERTY, 0.5f);
                }
                _allMaterials.AddRange(materials);
            }
            
            _isClipEnabled = true;
        }
    }
}