using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CurrencyConvertor.Behavior
{
    public class ListViewBaseSelectionChangedBehavior
    {
        public static ICommand GetSelectionChangedProperty(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(SelectionChangedProperty);
        }

        public static void SetSelectionChangedProperty(DependencyObject obj, ICommand value)
        {
            obj.SetValue(SelectionChangedProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectionChangedProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionChangedProperty =
            DependencyProperty.RegisterAttached("SelectionChangedProperty",
                                                 typeof(ICommand),
                                                 typeof(ListViewBaseSelectionChangedBehavior),
                                                 new PropertyMetadata(null, ExecuteSelectionChangedCommand));

        public static void ExecuteSelectionChangedCommand(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ListViewBase;
            if (control == null)
            {
                return;
            }
            if ((e.NewValue != null) && (e.OldValue == null))
            {
                control.SelectionChanged += (snd, args) =>
                {
                    var command = (snd as GridView).GetValue(ListViewBaseSelectionChangedBehavior.SelectionChangedProperty) as ICommand;
                    if (command != null)
                    {
                        command.Execute(args);
                    }
                };
            }
        }        
    }
}
