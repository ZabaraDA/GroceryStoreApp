using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WpfCustomControlLibrary
{
    [TemplatePart(Name = "PART_Button", Type = typeof(Button))]
    public class TextBoxWithRemoved : TextBox
    {
        
        static TextBoxWithRemoved()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxWithRemoved), new FrameworkPropertyMetadata(typeof(TextBoxWithRemoved)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var button = this.GetTemplateChild("PART_Button") as Button;

            if (button != null)
            {
                button.Click += button_Click;
            }      
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
        } 
    }
}
