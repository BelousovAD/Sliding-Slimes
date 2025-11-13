namespace Window
{
    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup))]
    internal class WindowView : MonoBehaviour
    {
        [SerializeField] private Window _window;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _window.ActivityChanged += UpdateView;
            _window.InteractableChanged += UpdateView;
        }

        private void OnEnable() =>
            UpdateView();

        private void OnDestroy()
        {
            _window.ActivityChanged -= UpdateView;
            _window.InteractableChanged -= UpdateView;
        }

        private void UpdateView()
        {
            if (_window.IsActive)
            {
                transform.SetAsLastSibling();
            }
            
            gameObject.SetActive(_window.IsActive);
            _canvasGroup.interactable = _window.IsInteractable;
        }
    }
}