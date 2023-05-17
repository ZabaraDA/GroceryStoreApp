using GroceryStoreApp.Databases;
using GroceryStoreApp.Models.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroceryStoreApp.Pages
{
    public partial class FormProductPage : Page
    {
        readonly GroceryStoreDatabasesEntities _databaseEntities = new GroceryStoreDatabasesEntities();

        private DateTime _supplyDateTime = DateTime.Now;

        private Товар _selectedProduct;

        private int _quantity = 12;
        private int _indient = 40;

        List<int> YearList = new List<int>();
        List<List<ТоварПоставка>> supplyList = new List<List<ТоварПоставка>>();




        string[] monthsName = new string[] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        public FormProductPage(Товар selectedProduct)
        {
            InitializeComponent();

            _selectedProduct = selectedProduct;
            DataContext = _selectedProduct;

            CreateRangeYears();
            CreateChart();

        }

        private void CreateChart()
        {

            if (_selectedProduct.ТоварПоставка.Count == 0)
            {
                InfoTextBlock.Visibility = Visibility.Visible;
                GenerateCanvas();
                return;
            }
            else
            {
                InfoTextBlock.Visibility = Visibility.Hidden;
            }


            ChartCanvas.Children.Clear();
            GenerateCanvas();

            double max = 1;

            double[] quantityProduct = new double[11];

            for (int i = 0; i < 11; i++)
            {
                
                supplyList.Add(_selectedProduct.ТоварПоставка.Where(x => x.Поставка.Статус == 3 && x.Поставка.ДатаПоставки.Year == Convert.ToInt16(ShowYearComboBox.SelectedItem) && x.Поставка.ДатаПоставки.Month == i).ToList());
                int summa = (int)supplyList[i].Sum(x => x.Количество);
                quantityProduct[i] = summa;
                if (summa > max)
                {
                    max = summa;
                }
            }

            double step = 1;
            while (max > 10)
            {

                max = Math.Ceiling(max / 10);
                step = step * 10;
            }
            step = Math.Ceiling(step / 10);
            int quantityInSegment = (int)(max * step);

            for (int i = 0; i < _quantity; i++)
            {
                TextBlock verticalTextBlock = new TextBlock()
                {
                    LayoutTransform = new ScaleTransform(1, -1),
                    RenderTransform = new TranslateTransform(0, _indient * (i) - 10 + _indient),
                    Text = (max * i * step).ToString(),
                    FontSize = 18,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Right,
                    Width = _indient * 2 - 5,
                };

                ChartCanvas.Children.Add(verticalTextBlock);
            }

            for (int i = 0; i < 11; i++)
            {
                StackPanel infoStackPanel = new StackPanel()
                {
                    Background = Brushes.White
                };

                TextBlock textBlock = new TextBlock()
                {
                    TextAlignment = TextAlignment.Center,
                    Height = double.NaN,
                    Padding = new Thickness(5),
                    Text = $"Поставка от {monthsName[i]} {ShowYearComboBox.Text}"
                };
                infoStackPanel.Children.Add(textBlock);
                #region MyRegion


                //string[] lines = new string[3] { "Дата","Количество","Остаток" };
                //StackPanel stackPanel2 = new StackPanel()
                //{
                //    Orientation = Orientation.Horizontal,
                //};
                //for (int j = 0; j < lines.Length; j++)
                //{
                //    TextBlock textBlock2 = new TextBlock()
                //    {
                //        Text = lines[j],
                //    };
                //    StackPanel stackPanel1 = new StackPanel();
                //    Border border = new Border()
                //    {
                //        Width = 200,
                //        Height = 40,
                //        Background = Brushes.White,
                //        BorderBrush = Brushes.Black,
                //        BorderThickness = new Thickness(1),
                //    };

                //    border.Child = textBlock2;
                //    stackPanel1.Children.Add(border);
                //    stackPanel2.Children.Add(stackPanel1);
                //}
                //infoStackPanel.Children.Add(stackPanel2);
                #endregion

                GridViewColumn dataColumn = new GridViewColumn()
                {
                    Width = 100,
                    DisplayMemberBinding = new Binding("Поставка.ДатаПоставки")
                    {
                        StringFormat = "MM-dd"
                    },
                    Header = "Дата",
                    
                };
                GridViewColumn quantityColumn = new GridViewColumn()
                {
                    Width = 150,
                    Header = "Количество",
                    DisplayMemberBinding = new Binding("Количество"),
                };
                GridViewColumn remainderColumn = new GridViewColumn()
                {
                    Width = 150,
                    Header = "Остаток",
                    DisplayMemberBinding = new Binding("Остаток"),
                   
                };
                GridViewColumn subsidiaryColumn = new GridViewColumn()
                {
                    
                    Width = 200,
                    Header = "Филиал",
                    DisplayMemberBinding = new Binding("Поставка.Филиал.Наименование"),
                };
                GridView gridView = new GridView();
                gridView.Columns.Add(dataColumn);
                gridView.Columns.Add(quantityColumn);
                gridView.Columns.Add(remainderColumn);
                gridView.Columns.Add(subsidiaryColumn);

                ListView listView = new ListView()
                {
                    View = gridView,
                    Height = 300,
                    BorderThickness = new Thickness(0),
                    Background = Brushes.Transparent,
                    ItemsSource = supplyList[i],
                };

                infoStackPanel.Children.Add(listView);

                StackPanel stackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    RenderTransform = new TranslateTransform
                    (_indient * 2 * (12 - i), _indient),
                    ToolTip = infoStackPanel,
                    VerticalAlignment = VerticalAlignment.Bottom,
                };
                Border deliveryBorder = new Border()
                {
                    CornerRadius = new CornerRadius(0,0,10,10),
                    Width = _indient,
                    Height = (double)(supplyList[i].Sum(x => x.Количество) * _indient / quantityInSegment),
                    Background = Application.Current.FindResource("Chart.Delivery.Background") as Brush,
                    VerticalAlignment = VerticalAlignment.Bottom,
                };

                Border remainderBorder = new Border()
                {
                    CornerRadius = new CornerRadius(0, 0, 10, 10),
                    Width = _indient,
                    Height = (double)(supplyList[i].Sum(x => x.Остаток) * _indient / quantityInSegment),
                    Background = Application.Current.FindResource("Chart.Remainder.Background") as Brush,
                    VerticalAlignment = VerticalAlignment.Top,
                };

                DoubleAnimation buttonAnimation = new DoubleAnimation();
                buttonAnimation.From = 0;
                buttonAnimation.To = (double)(supplyList[i].Sum(x => x.Количество) * _indient / quantityInSegment);
                buttonAnimation.Duration = TimeSpan.FromSeconds(1);

                deliveryBorder.BeginAnimation(HeightProperty, buttonAnimation);
                buttonAnimation.To = (double)(supplyList[i].Sum(x => x.Остаток) * _indient / quantityInSegment); // остаток
                remainderBorder.BeginAnimation(HeightProperty, buttonAnimation);

                stackPanel.Children.Add(deliveryBorder);
                stackPanel.Children.Add(remainderBorder);

                ChartCanvas.Children.Add(stackPanel);
            }
        }

        private void GenerateCanvas()
        {
            for (int i = 0; i < _quantity; i++)
            {
                TextBlock horizontalTextBlock = new TextBlock()
                {
                    Text = monthsName[_quantity - i - 1].ToString(),
                    LayoutTransform = new ScaleTransform(1, -1),
                    RenderTransform = new TranslateTransform(_indient * (i + 1) * 2, 0),
                    FontSize = _indient / 2,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    Width = _indient * 2,
                    Height = _indient

                };

                Line horizontalLine = new Line()
                {
                    X1 = _indient,
                    X2 = _indient * _quantity * 2 + _indient * 2,
                    Y1 = _indient * i + _indient,
                    Y2 = _indient * i + _indient,
                    StrokeThickness = 1,
                    Opacity = 50,
                    Stroke = Brushes.Gray,
                };
                Line verticalLLine = new Line()
                {
                    Y1 = _indient - _indient,
                    Y2 = _indient * _quantity + _indient,
                    X1 = _indient * i * 2 + _indient * 2,
                    X2 = _indient * i * 2 + _indient * 2,
                    StrokeThickness = 1,
                    Opacity = 50,
                    Stroke = Brushes.Gray
                };

                ChartCanvas.Children.Add(horizontalTextBlock);
                ChartCanvas.Children.Add(horizontalLine);
                ChartCanvas.Children.Add(verticalLLine);

            }
        }

        private void CreateRangeYears() //временной диапазон поставок товара
        {                               //от года первой поставки до текущего года

            if (_databaseEntities.Поставка.Count() > 0)
            {
                DateTime firstDeliveryDate = _databaseEntities.Поставка.Min(x => x.ДатаПоставки);
                int timeRange = _supplyDateTime.Year - firstDeliveryDate.Year;
                for (int i = 0; i < timeRange + 1; i++)
                {
                    YearList.Add(firstDeliveryDate.Year + i);
                }
            }
            ShowYearComboBox.ItemsSource = YearList.ToList();
            ShowYearComboBox.SelectedIndex = 0;
        }

        private void ShowYearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateChart();
        }
    }
}

