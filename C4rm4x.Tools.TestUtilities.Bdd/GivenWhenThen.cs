#region Using

using C4rm4x.Tools.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

#endregion

namespace C4rm4x.Tools.TestUtilities.Bdd
{
    /// <summary>
    /// Implementation of a Given-When-Then test plan
    /// </summary>
    public class GivenWhenThen :
        IGivenDefinition, IWhenDefinition, IThenDefinition
    {
        private readonly ICollection<GivenStep> _givenSteps;
        private readonly ICollection<WhenStep> _whenSteps;
        private readonly ICollection<ThenStep> _thenSteps;

        /// <summary>
        /// Constructor
        /// </summary>
        private GivenWhenThen()
        {
            _givenSteps = new List<GivenStep>();
            _whenSteps = new List<WhenStep>();
            _thenSteps = new List<ThenStep>();
        }

        /// <summary>
        /// Adds a then step
        /// </summary>
        /// <param name="thenStep">The then step</param>
        /// <param name="description">Description of the assertion</param>
        public IThenDefinition Then(
            ThenHandler thenStep,
            string description)
        {
            thenStep.NotNull(nameof(thenStep));

            _thenSteps.Add(new ThenStep(thenStep, description));

            return this;
        }

        /// <summary>
        /// Adds another given step
        /// </summary>
        /// <param name="givenStep">The given step</param>
        public IGivenDefinition And(GivenHandler givenStep)
        {
            Given(givenStep);

            return this;
        }

        /// <summary>
        /// Adds a when step
        /// </summary>
        /// <param name="whenStep">The when step</param>
        public IWhenDefinition When(WhenHandler whenStep)
        {
            whenStep.NotNull(nameof(whenStep));

            _whenSteps.Add(new WhenStep(whenStep));

            return this;
        }

        /// <summary>
        /// Adds another when step
        /// </summary>
        /// <param name="whenStep">The when step</param>
        public IWhenDefinition And(WhenHandler whenStep)
        {
            When(whenStep);

            return this;
        }

        /// <summary>
        /// Adds another then step
        /// </summary>
        /// <param name="thenStep">The then step</param>
        /// <param name="description">Description of the assertion</param>
        public IThenDefinition And(
            ThenHandler thenStep,
            string description)
        {
            Then(thenStep, description);

            return this;
        }

        /// <summary>
        /// Confirms all your then steps
        /// </summary>
        public void Confirm()
        {
            _thenSteps.Must(
                c => !c.IsNullOrEmpty(),
                "At least one \"Then\" must be specified");

            foreach (var givenStep in _givenSteps)
                givenStep.Setup();

            foreach (var whenStep in _whenSteps)
                whenStep.Execute();

            foreach (var thenStep in _thenSteps)
                Assert.IsTrue(thenStep.IsSuccess(),
                    "Failed while asserting: {0}".AsFormat(thenStep.Description));
        }

        /// <summary>
        /// Creates a new instance of GivenWhenThen with the initial given step
        /// </summary>
        /// <param name="givenStep">The initial given step</param>
        /// <returns>A new instance of GivenWhenThen</returns>
        public static GivenWhenThen StartWith(GivenHandler givenStep)
        {
            var instance = new GivenWhenThen();

            instance.Given(givenStep);

            return instance;
        }

        private void Given(GivenHandler givenStep)
        {
            givenStep.NotNull(nameof(givenStep));

            _givenSteps.Add(new GivenStep(givenStep));
        }

        /// <summary>
        /// Creates a new instance of GivenWhenThen with the initial when step
        /// </summary>
        /// <param name="whenStep">The initial when step</param>
        /// <returns>A new instance of GivenWhenThen</returns>
        public static GivenWhenThen StartWith(WhenHandler whenStep)
        {
            var instance = new GivenWhenThen();

            instance.When(whenStep);

            return instance;
        }

        #region Internal classes

        internal class GivenStep
        {
            private readonly GivenHandler _executor;

            public GivenStep(GivenHandler executor)
            {
                executor.NotNull(nameof(executor));

                _executor = executor;
            }

            public void Setup()
            {
                _executor();
            }
        }

        internal class ThenStep
        {
            private readonly ThenHandler _predicate;

            public string Description { get; private set; }

            public ThenStep(
                ThenHandler predicate,
                string description)
            {
                predicate.NotNull(nameof(predicate));
                description.NotNullOrEmpty(nameof(description));

                _predicate = predicate;
                Description = description;
            }

            public bool IsSuccess()
            {
                return _predicate();
            }
        }

        internal class WhenStep
        {
            private readonly WhenHandler _executor;

            public WhenStep(WhenHandler executor)
            {
                executor.NotNull(nameof(executor));

                _executor = executor;
            }

            public void Execute()
            {
                _executor();
            }
        }

        #endregion
    }
}
