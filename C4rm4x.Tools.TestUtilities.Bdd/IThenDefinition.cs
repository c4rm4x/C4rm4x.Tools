namespace C4rm4x.Tools.TestUtilities.Bdd
{
    /// <summary>
    /// Configure your then steps
    /// </summary>
    public interface IThenDefinition
    {
        /// <summary>
        /// Adds another then step
        /// </summary>
        /// <param name="thenStep">Then then step</param>
        /// <param name="description">Description of the assertion</param>
        IThenDefinition And(ThenHandler thenStep, string description);

        /// <summary>
        /// Confirms all your then steps
        /// </summary>
        void Confirm();
    }
}
