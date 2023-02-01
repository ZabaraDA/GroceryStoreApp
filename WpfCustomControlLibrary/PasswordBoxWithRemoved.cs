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

namespace WpfCustomControlLibrary
{
    /// <summary>
    /// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот пользовательский элемент управления в файле XAML.
    ///
    /// Шаг 1a. Использование пользовательского элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary"
    ///
    ///
    /// Шаг 1б. Использование пользовательского элемента управления в файле XAML, существующем в другом проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary;assembly=WpfCustomControlLibrary"
    ///
    /// Потребуется также добавить ссылку из проекта, в котором находится файл XAML,
    /// на данный проект и пересобрать во избежание ошибок компиляции:
    ///
    ///     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    ///     "Добавить ссылку"->"Проекты"->[Поиск и выбор проекта]
    ///
    ///
    /// Шаг 2)
    /// Теперь можно использовать элемент управления в файле XAML.
    ///
    ///     <MyNamespace:PasswordBoxWithRemoved/>
    ///
    /// </summary>
    [TemplatePart(Name = "PART_ContentHost", Type = typeof(FrameworkElement))]
    public class PasswordBoxWithRemoved : TextBox
    {
        static PasswordBoxWithRemoved()
        {
           
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBoxWithRemoved), new FrameworkPropertyMetadata(typeof(PasswordBoxWithRemoved)));
     
        }
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
       "Password", typeof(PasswordPropertyTextAttribute), typeof(PasswordBoxWithRemoved), new PropertyMetadata(default(PasswordPropertyTextAttribute)));

        public PasswordPropertyTextAttribute Password { get; set; }

        public static readonly DependencyProperty PasswordCharProperty = DependencyProperty.Register(
        "PasswordChar", typeof(char), typeof(PasswordBoxWithRemoved), new PropertyMetadata(default(char)));

       

        public char PasswordChar
        {
            get
            {
                return (char)GetValue(PasswordCharProperty);
            }
            set
            {
                SetValue(PasswordCharProperty, value);
            }
        }
    }
}
