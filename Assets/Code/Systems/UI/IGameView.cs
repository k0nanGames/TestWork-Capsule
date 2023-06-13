namespace TestWork.UI
{
    public interface IGameView
    {
        /// <summary>
        /// Init all needed parameters
        /// </summary>
        void Init();

        /// <summary>
        /// Show this view
        /// </summary>
        void Show();

        /// <summary>
        /// Hide this view
        /// </summary>
        void Hide();

        /// <summary>
        /// Returns type of current view
        /// </summary>
        /// <returns></returns>
        UIViewType GetViewType();
    }
}