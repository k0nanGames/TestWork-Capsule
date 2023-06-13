namespace TestWork.Characters
{
    public interface IGameEnemy
    {
        /// <summary>
        /// Init AI, when bot spawned
        /// </summary>
        void Init();

        /// <summary>
        /// Use it for destroy enemy object
        /// </summary>
        void Die();
    }
}