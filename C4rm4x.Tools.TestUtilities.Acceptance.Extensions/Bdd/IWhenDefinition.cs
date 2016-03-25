namespace C4rm4x.Tools.TestUtilities.Bdd
{
    /// <summary>
    /// Configure your when steps
    /// </summary>
    public interface IWhenDefinition : IStartingDefinition
    {
        /// <summary>
        /// Adds another when step
        /// </summary>
        /// <param name="whenStep">The when step</param>
        IWhenDefinition And(WhenHandler whenStep);
    }
}
