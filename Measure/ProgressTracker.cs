using System;

namespace CsBenchmark.Measure
{
    /// <summary>
    /// A performance progress tracker, call the Step() method to indicate that progress has occured and this class will determine when to call a progress callback
    /// </summary>
    public class ProgressTracker
    {
        private long totalSteps;
        private long currentStep;
        private double lastCbPercentage;
        private double cbPercentageInterval;
        private readonly Action<double> progressCb;


        public ProgressTracker(double cbPercentageInterval, Action<double> progressCallback)
        {
            if (cbPercentageInterval <= 0 || cbPercentageInterval > 1)
            {
                throw new ArgumentOutOfRangeException("cbPercentageInterval", cbPercentageInterval, "progress percentage interval must be between (0.0, 1.0]");
            }
            this.totalSteps = 1;
            this.currentStep = 0;
            this.lastCbPercentage = 0;
            this.cbPercentageInterval = cbPercentageInterval;
            this.progressCb = progressCallback;
        }


        /// <summary>Reset the current number of completed steps to 0 and set the total number of steps</summary>
        /// <param name="totalSteps">The new total number of steps</param>
        public void ResetTotalSteps(long totalSteps)
        {
            this.currentStep = 0;
            this.totalSteps = totalSteps;
        }


        /// <summary>Progress has occurred, the number of steps completed defaults to 1, a custom number can be provided</summary>
        /// <param name="steps">The number of steps completed, defaults to 1</param>
        public void Step(long steps = 1)
        {
            this.currentStep += steps;

            var nextCbPercentage = this.lastCbPercentage + this.cbPercentageInterval;
            var nextCbStep = nextCbPercentage * this.totalSteps;
            nextCbStep = nextCbStep > this.totalSteps ? this.totalSteps : nextCbStep;

            if (this.currentStep >= nextCbStep)
            {
                progressCb(nextCbPercentage);
                this.lastCbPercentage = nextCbPercentage;
            }
        }

    }
}
