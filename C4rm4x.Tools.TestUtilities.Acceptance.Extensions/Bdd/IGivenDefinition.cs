namespace C4rm4x.Tools.TestUtilities.Bdd
{
    /// <summary>
    /// Configure your given steps
    /// </summary>
    public interface IGivenDefinition : IStartingDefinition
    {
        /// <summary>
        /// Adds another given step
        /// </summary>
        /// <param name="givenStep">The given step</param>        
        IGivenDefinition And(GivenHandler givenStep);

        /// <summary>
        /// Adds a when step
        /// </summary>
        /// <param name="whenStep">Then when step</param>
        IWhenDefinition When(WhenHandler whenStep);
    }
}
