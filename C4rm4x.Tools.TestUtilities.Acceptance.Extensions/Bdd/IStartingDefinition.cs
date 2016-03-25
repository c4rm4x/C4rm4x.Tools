namespace C4rm4x.Tools.TestUtilities.Bdd
{
    /// <summary>
    /// Entry point for a given-when-then test plan
    /// </summary>
    public interface IStartingDefinition
    {
        /// <summary>
        /// Adds a then step
        /// </summary>
        /// <param name="thenStep">The then step</param>
        /// <param name="description">Description of the assertion</param>
        IThenDefinition Then(ThenHandler thenStep, string description);
    }
}
