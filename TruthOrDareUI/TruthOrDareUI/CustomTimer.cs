using System;
using System.Threading;
using Xamarin.Forms;

namespace TruthOrDareUI
{
    /// <summary>
    /// A class that is used for timers.
    /// </summary>
    public class CustomTimer
    {
        private readonly TimeSpan _timeSpan;
        private readonly Action _action;
        private CancellationTokenSource _cancellation = new CancellationTokenSource();

        /// <summary>
        /// Creates a new instance of a timer.
        /// </summary>
        /// <param name="interval">The interval of the timer</param>
        /// <param name="action">The action to invoke everytime the timer elapses.</param>
        public CustomTimer(Action action)
        {
            _timeSpan = TimeSpan.FromMilliseconds(1000);
            _action = action;
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            CancellationTokenSource cts = _cancellation;

            Device.StartTimer(_timeSpan, () =>
            {
                if (cts.IsCancellationRequested)
                {
                    return false;
                }

                _action.Invoke();
                return true;
            });
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void Stop()
        {
            Interlocked.Exchange(ref _cancellation, new CancellationTokenSource()).Cancel();
        }
    }
}