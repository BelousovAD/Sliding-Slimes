namespace Leaderboard
{
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    internal class PositionView : MonoBehaviour
    {
        [SerializeField] private LeaderboardItem _item;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _textField;
        [SerializeField] private List<Sprite> _positionSprites = new();

        private void OnEnable() =>
            _item.Initialized += UpdateView;

        private void OnDisable() =>
            _item.Initialized -= UpdateView;

        private void Start() =>
            UpdateView();

        private void UpdateView()
        {
            if (_item.Position <= _positionSprites.Count)
            {
                _textField.enabled = false;
                _image.sprite = _positionSprites[_item.Position - 1];
            }
            else
            {
                _image.enabled = false;
                _textField.text = _item.Position.ToString();
            }
        }
    }
}