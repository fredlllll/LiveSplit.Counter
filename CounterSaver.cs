using System;
using System.IO;
using System.Text;
using LiveSplit.Model;
using LiveSplit.UI.Components;

namespace LiveSplit.Counter
{
    class CounterSaver
    {
        private readonly UI.Components.ICounter counter;
        private readonly LiveSplitState state;

        public CounterSaver(UI.Components.ICounter counter, LiveSplitState state)
        {
            this.counter = counter;
            counter.CounterChanged += Counter_CounterChanged;
            this.state = state;

            Load();
        }

        private void Counter_CounterChanged(UI.Components.Counter sender, CounterChangedEventArgs args)
        {
            Save();
        }

        private string AppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private string CounterFile => Path.Combine(AppData, state.Run.Metadata.RunID + ".txt");

        void Save()
        {
            File.WriteAllText(CounterFile, counter.Count.ToString(System.Globalization.CultureInfo.InvariantCulture), Encoding.UTF8);
        }

        void Load()
        {
            var counterFile = CounterFile;
            if(File.Exists(counterFile))
            {
                string content = File.ReadAllText(counterFile);
                if(int.TryParse(content, out int count))
                {
                    counter.SetCount(count);
                }
            }
        }
    }
}
