namespace Book.ViewModels.Samples.Chapter18.Sample06
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using ReactiveUI;
    using Splat;

    // Some useful extensions to turn activatable objects into an IObservable<bool>.
    public static class GetIsActivatedExtensions
    {
        public static IObservable<bool> GetIsActivated(this IActivatableViewModel @this) =>
            Observable
                .Merge(
                    @this.Activator.Activated.Select(_ => true),
                    @this.Activator.Deactivated.Select(_ => false))
                .Replay(1)
                .RefCount();

        public static IObservable<bool> GetIsActivated(this IActivatableView @this)
        {
            var activationForViewFetcher = Locator.Current.GetService<IActivationForViewFetcher>();
            return activationForViewFetcher
                .GetActivationForView(@this)
                .Replay(1)
                .RefCount();
        }
    }
}