using GroceryStoreApp.Databases;
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
    /// <summary>
    /// Логика взаимодействия для FormProductPage.xaml
    /// </summary>
    public partial class FormProductPage : Page
    {
        GroceryStoreDatabasesEntities databaseEntities = new GroceryStoreDatabasesEntities();

        DateTime month = DateTime.Now;

        private Товар _selectedProduct;

        private int _quantity = 12;
        private int _indient = 30;
        private int[] _year;


        string[] monthsName = new string[] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        public FormProductPage(Товар selectedProduct)
        {
            InitializeComponent();
            _selectedProduct = selectedProduct;
            DataContext = _selectedProduct;

            CreateRangeYears();
            //GenerateCanvas();
            CreateChart();

        }

        private void CreateChart()
        {
            List<ТоварПоставка> deliverySelectedProductList = databaseEntities.ТоварПоставка.ToList().Where(x => x.КодТовара.Equals(_selectedProduct.Код)).Where(x => x.Поставка.ДатаПоставки.Year.Equals(Convert.ToInt16(ShowYearComboBox.Text))).ToList();

            if (deliverySelectedProductList.Count == 0)
            {
                MessageBox.Show("Нет поставок");
                return;
            }

            ChartCanvas.Children.Clear();
            GenerateCanvas();

            int max = deliverySelectedProductList.Max(x => x.Количество);
            int step = 1;

            while (max > 10)
            {
                max = max / 10;
                step = step * 10;
            }
            step = step / 10;
            int quantityInSegment = max * step;


            for (int i = 0; i < _quantity; i++)
            {
                TextBlock verticalTextBlock = new TextBlock()
                {
                    //FlowDirection = FlowDirection.RightToLeft,
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



            for (int i = 0; i < deliverySelectedProductList.Count; i++)
            {
                var deliveryProduct = deliverySelectedProductList.Where(x => x.Поставка.Equals(deliverySelectedProductList[i].Поставка)).FirstOrDefault();
                if (deliveryProduct == null)
                {
                    return;
                }

                Grid grid = new Grid();

                TextBlock textBlock = new TextBlock()
                {
                    Text = "Поставка от " + monthsName[deliveryProduct.Поставка.ДатаПоставки.Month-1] + deliveryProduct.Поставка.ДатаПоставки.Year.ToString() ,
                };
                grid.Children.Add(textBlock);

                StackPanel stackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    RenderTransform = new TranslateTransform
                    (_indient * 2 + _indient * 2 * (_quantity - deliverySelectedProductList[i].Поставка.ДатаПоставки.Month),
                    ((_indient * _quantity) - deliveryProduct.Количество * _indient / quantityInSegment) * 0 + _indient),

                    //ToolTip = $"{deliveryProduct.Поставка.ДатаПоставки} доставлен {_selectedProduct.Наименование}" +
                    //$"\n в количестве {deliveryProduct.Количество} {_selectedProduct.ЕдиницаИзмерения.Аббревиатура} " +
                    //$"\n Остаток с поставки {deliveryProduct.Остаток} {_selectedProduct.ЕдиницаИзмерения.Аббревиатура}",
                    ToolTip = grid,
                    VerticalAlignment = VerticalAlignment.Bottom,
                };
                Rectangle deliveryRectangle = new Rectangle()
                {
                    Width = _indient,
                    Height = deliveryProduct.Количество * _indient / quantityInSegment,
                    Fill = Brushes.Violet,
                    VerticalAlignment = VerticalAlignment.Bottom,
                };

                Rectangle remainderRectangle = new Rectangle()
                {
                    Width = _indient,
                    Height = deliveryProduct.Остаток * _indient / quantityInSegment,
                    Fill = Brushes.DarkCyan,
                    VerticalAlignment = VerticalAlignment.Top,
                };

                InfoTextBlock.Text = stackPanel.RenderTransform.Value.ToString();

                DoubleAnimation buttonAnimation = new DoubleAnimation();
                buttonAnimation.From = 0;
                buttonAnimation.To = deliveryProduct.Количество * _indient / quantityInSegment;
                buttonAnimation.Duration = TimeSpan.FromSeconds(1);

                deliveryRectangle.BeginAnimation(HeightProperty, buttonAnimation);
                buttonAnimation.To = deliveryProduct.Остаток * _indient / quantityInSegment;
                remainderRectangle.BeginAnimation(HeightProperty, buttonAnimation);

                stackPanel.Children.Add(deliveryRectangle);
                stackPanel.Children.Add(remainderRectangle);

                ChartCanvas.Children.Add(stackPanel);
            }
        }

        private void GenerateCanvas()
        {
            for (int i = 0; i < _quantity; i++)
            {
                TextBlock horizontal = new TextBlock()
                {
                    Text = monthsName[_quantity - i - 1].ToString(),
                    LayoutTransform = new ScaleTransform(1, -1),
                    //LayoutTransform = new RotateTransform(315),
                    RenderTransform = new TranslateTransform(_indient * (i + 1) * 2,0),
                    FontSize = _indient/2,
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
                    Fill = Brushes.Black,
                    StrokeThickness = 1,
                    Stroke = Brushes.Azure,
                };
                Line verticalLLine = new Line()
                {
                    Y1 = _indient - _indient,
                    Y2 = _indient * _quantity + _indient,
                    X1 = _indient * i * 2 + _indient * 2,
                    X2 = _indient * i * 2 + _indient * 2,
                    Fill = Brushes.Aquamarine,
                    StrokeThickness = 1,
                    Stroke = Brushes.Firebrick
                };

                ChartCanvas.Children.Add(horizontal);
                ChartCanvas.Children.Add(horizontalLine);
                ChartCanvas.Children.Add(verticalLLine);

            }
        }

        private void CreateRangeYears() //временной диапазон поставок товара
        {                               //от года первой поставки до текущего года
            DateTime firstDeliveryDate = databaseEntities.Поставка.Min(x => x.ДатаПоставки);
            int timeRange = month.Year - firstDeliveryDate.Year;
            InfoTextBlock.Text = timeRange.ToString();
            _year = new int[timeRange + 1];
            for (int i = 0; i < _year.Length; i++)
            {
                _year[i] = firstDeliveryDate.Year + i;
            }
            ShowYearComboBox.ItemsSource = _year;
            ShowYearComboBox.SelectedIndex = 0;
        }

        private void ShowYearComboBox_DropDownClosed(object sender, EventArgs e)
        {
            CreateChart();
        }

        private void ChartCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            int xMousePosition = Convert.ToInt16(e.GetPosition(ChartCanvas).X);
            int yMousePosition = Convert.ToInt16(e.GetPosition(ChartCanvas).Y);
            txtbX.Text = xMousePosition.ToString();
            txtbY.Text = yMousePosition.ToString();
        }

        private void ZoomOutChartBtton_Click(object sender, RoutedEventArgs e)
        {
            
            _indient = _indient - 10;
            ChartCanvas.Height = ChartCanvas.Height - 10 * 12;
            CreateChart();
        }

        private void ZoomInChartBtton_Click(object sender, RoutedEventArgs e)
        {
            _indient = _indient + 10;
            ChartCanvas.Height = ChartCanvas.Height + 10*12;
            CreateChart();
        }
    }
}

