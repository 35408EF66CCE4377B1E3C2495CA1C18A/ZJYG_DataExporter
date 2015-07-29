using System.ComponentModel;

namespace DataExporter.Classes
{
    public class CommonBase : INotifyPropertyChanged
    {

        public  event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string PropertyName)
        {
            PropertyChangedEventArgs arg = new PropertyChangedEventArgs(PropertyName);
            if (PropertyChanged != null)
                PropertyChanged(this, arg);
        }
    }
}
