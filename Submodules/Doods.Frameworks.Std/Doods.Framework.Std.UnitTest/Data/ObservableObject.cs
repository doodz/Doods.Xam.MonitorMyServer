using System;

namespace Doods.Framework.Std.UnitTest.Data
{
    public class ObservableObject : NotifyPropertyChangedBase
    {
        public Action Changed { get; set; }

        public Func<string, string, bool> Validate { get; set; }

        private string _propertyOne;

        public string PropertyOne
        {
            get => _propertyOne;
            set => SetProperty(ref _propertyOne, value, Changed, Validate);
        }

        private int _propertyTwo;

        public int PropertyTwo
        {
            get => _propertyTwo;
            set => SetProperty(ref _propertyTwo, value, Changed,null);
        }
    }
}