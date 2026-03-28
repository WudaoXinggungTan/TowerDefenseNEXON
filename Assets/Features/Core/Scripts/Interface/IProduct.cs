namespace Features.Core.Scripts.Interface
{
    public interface IProduct
    {
        #region Variables

        public string ProductName   { get; }
        public bool   IsInitialized { get; }

        #endregion

        #region Public Methods

        public void Initialize();

        #endregion
    }
}