using GroceryStoreApp.Pages;
using GroceryStoreApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GroceryStoreApp.ViewModels
{
    public class MenuViewModel : ViewModelBase /*: BindableBase*/
    {
        private readonly PageService _pageService;


        private Page _pageSource;
        public Page PageSource 
        {
            get
            {
                return _pageSource;
            } 
            set
            {
                _pageSource = value;
                OnPropertyChanged(nameof(_pageSource));
            } 
        }


        public MenuViewModel(PageService pageService)
        {
            _pageService = pageService;

            _pageService.OnPageChanged += (page) => PageSource = page;
            _pageService.ChangePage(new WelcomePage());
        }
        public MenuViewModel()
        {
            //_pageService = new PageService();
            //_pageService.ChangePage(new WelcomePage());
            _pageSource = new WelcomePage();
        }

       
    }
}
