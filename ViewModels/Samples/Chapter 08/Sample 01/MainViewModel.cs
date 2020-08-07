using DynamicData;

namespace Book.ViewModels.Samples.Chapter08.Sample01
{
    using System.Linq;
    using System.Reactive;
    using System.Reactive.Linq;
    using ReactiveUI;

    [Sample(
        "Synchronous Command",
        @"This sample demonstrates the use of `ReactiveCommand` to create synchronous commands.

Both buttons are backed by a `ReactiveCommand<Unit, Unit>`, and the logic for the buttons executes synchronously.")]
    public sealed class MainViewModel : ReactiveObject
    {
        private readonly SourceList<string> dinosaurs;
        private readonly ReactiveCommand<Unit, Unit> addDinosaurCommand;
        private readonly ReactiveCommand<Unit, Unit> deleteDinosaurCommand;
        private string name;
        private string selectedDinosaur;

        public MainViewModel()
        {
            this.dinosaurs = new SourceList<string>(
                Data
                .Dinosaurs
                .All
                .Select(dinosaur => dinosaur.Name)
                .AsObservableChangeSet()
                );

            this.addDinosaurCommand = ReactiveCommand.Create(
                () => this.dinosaurs.Add(this.Name));
            this.deleteDinosaurCommand = ReactiveCommand.Create(
                () => { this.dinosaurs.Remove(this.SelectedDinosaur); });
        }

        public IObservableList<string> Dinosaurs => this.dinosaurs;

        public ReactiveCommand<Unit, Unit> AddDinosaurCommand => this.addDinosaurCommand;

        public ReactiveCommand<Unit, Unit> DeleteDinosaurCommand => this.deleteDinosaurCommand;

        public string Name
        {
            get => this.name;
            set => this.RaiseAndSetIfChanged(ref this.name, value);
        }

        public string SelectedDinosaur
        {
            get => this.selectedDinosaur;
            set => this.RaiseAndSetIfChanged(ref this.selectedDinosaur, value);
        }
    }
}
