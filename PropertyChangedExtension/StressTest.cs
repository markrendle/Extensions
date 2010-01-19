using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace PropertyChangedExtension
{
    public static class StressTest
    {
        static FieldInfo field = typeof(Foo).GetField("PropertyChanged", BindingFlags.NonPublic | BindingFlags.Instance);
        static object typedRef;

        public static void RaiseEvent(INotifyPropertyChanged sender, string propertyName)
        {
            TypedReference typedRef = __makeref(sender);
            var handler = field.GetValueDirect(typedRef) as PropertyChangedEventHandler;

            if (handler != null)
            {
                handler(sender, new PropertyChangedEventArgs("Name"));
            }
        }
    }
}
