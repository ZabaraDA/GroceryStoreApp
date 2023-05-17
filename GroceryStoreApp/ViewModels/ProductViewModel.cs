using GroceryStoreApp.Command;
using GroceryStoreApp.CsClasses;
using GroceryStoreApp.Databases;
using GroceryStoreApp.Models;
using GroceryStoreApp.Models.Databases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

namespace GroceryStoreApp.ViewModels
{
    class ProductViewModel : ViewModelBase
    {
        private readonly ProductModel _productModel = new ProductModel();
        private Товар _currentProduct = new Товар();
        public ObservableCollection<Товар> ProductList { get; set; } = new ObservableCollection<Товар>();

        public ProductViewModel()
        {
            foreach (var product in _productModel.GetAllProduct())
            {
                ProductList.Add(product);
            }
        }
        

        #region ProductProperty
        public int Id
        {
            get
            {
                return _currentProduct.Код;
            }
            set
            {
                _currentProduct.Код = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get
            {
                return _currentProduct.Наименование;
            }
            set
            {
                _currentProduct.Наименование = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get
            {
                return _currentProduct.Описание;
            }
            set
            {
                _currentProduct.Описание = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Article
        {
            get
            {
                return _currentProduct.Артикул;
            }
            set
            {
                _currentProduct.Артикул = value;
                OnPropertyChanged(nameof(Article));
            }
        }

        public string Barcode
        {
            get
            {
                return _currentProduct.ШтрихКод;
            }
            set
            {
                _currentProduct.ШтрихКод = value;
                OnPropertyChanged(nameof(Barcode));
            }
        }

        public byte VAT
        {
            get
            {
                return _currentProduct.НДС;
            }
            set
            {
                _currentProduct.НДС = value;
                OnPropertyChanged(nameof(VAT));
            }
        }
        public decimal Cost
        {
            get
            {
                return _currentProduct.Цена;
            }
            set
            {
                _currentProduct.Цена = value;
                OnPropertyChanged(nameof(Cost));
            }
        } 
        public int ExpirationDate
        {
            get
            {
                return _currentProduct.СрокГодности;
            }
            set
            {
                _currentProduct.СрокГодности = value;
                OnPropertyChanged(nameof(ExpirationDate));
            }
        }
        public bool Status
        {
            get
            {
                return _currentProduct.Статус;
            }
            set
            {
                _currentProduct.Статус = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public decimal Quantity
        {
            get
            {
                return _currentProduct.Количество;
            }
            set
            {
                _currentProduct.Количество = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public decimal Weight
        {
            get
            {
                return _currentProduct.Вес;
            }
            set
            {
                _currentProduct.Вес = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        public int Width
        {
            get
            {
                return _currentProduct.Ширина;
            }
            set
            {
                _currentProduct.Ширина = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        public int Height
        {
            get
            {
                return _currentProduct.Высота;
            }
            set
            {
                _currentProduct.Высота = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        public int Depth
        {
            get
            {
                return _currentProduct.Глубина;
            }
            set
            {
                _currentProduct.Глубина = value;
                OnPropertyChanged(nameof(Depth));
            }
        }

        public byte[] Photo
        {
            get { return _currentProduct.Фото; }
            set
            {
                _currentProduct.Фото = value;
                OnPropertyChanged(nameof(Photo));
            }
        }
        #endregion

        #region Commands
        public ICommand AddProductCommand
        {
            get
            {
                return new ActionCommand((obj) =>
                {
                    _productModel.AddOrUpdateProduct(_currentProduct);
                });
            }
        }
        public ICommand AddPhotoCommand
        {
            get
            {
                return new ActionCommand((obj) =>
                {
                    Photo = PhotoImportClass.ImportToByte(800);
                });
            }
        }
        public ICommand ClearPhotoCommand
        {
            get
            {
                return new ActionCommand((obj) =>
                {
                    Photo = null;
                }, (obj) => Photo != null);
            }
        }
        #endregion
    }
}
