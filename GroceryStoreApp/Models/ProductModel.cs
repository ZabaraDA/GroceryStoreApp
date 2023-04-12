using GroceryStoreApp.Databases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static GroceryStoreApp.Models.ProductModel;

namespace GroceryStoreApp.Models
{

    //public class ProjectEventArgs : EventArgs
    //{
    //    public Товар Product { get; set; }
    //    public ProjectEventArgs(Товар product)
    //    {
    //        Product = product;
    //    }
    //}
    public interface IProductModel
    {
        ObservableCollection<Товар> ProductList { get; set; }
        //event EventHandler<ProjectEventArgs> ProjectUpdated;
        //void UpdateProject(Товар updatedProject);
    }
    public class ProductModel
    {
        private readonly GroceryStoreDatabasesEntities _databasesEntities = new GroceryStoreDatabasesEntities();

        //public ObservableCollection<Товар> ProductList { get; set; }
        //public event EventHandler<ProjectEventArgs> ProjectUpdated = delegate { };

        //public ProductModel()
        //{
        //    ProductList = new ObservableCollection<Товар>();
        //    foreach (Товар product in _databasesEntities.Товар)
        //    {
        //        ProductList.Add(product);
        //    }
        //}

        public void AddOrUpdateProduct(Товар currentProduct)
        {
            try
            {
                _databasesEntities.Товар.AddOrUpdate(currentProduct);
                _databasesEntities.SaveChanges();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        //public void UpdateProject(Товар updatedProject)
        //{
        //    GetProject(updatedProject.Код).Update(updatedProject);
        //    ProjectUpdated(this,
        //        new ProjectEventArgs(updatedProject));
        //}

        //private Товар GetProject(int productId)
        //{
        //    return ProductList.FirstOrDefault(
        //        product => product.Код == productId);
        //}

        public List<Товар> GetAllProduct()
        {
            return _databasesEntities.Товар.ToList();
        }

    }
}
