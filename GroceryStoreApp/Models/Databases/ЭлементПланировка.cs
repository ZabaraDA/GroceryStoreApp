//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroceryStoreApp.Models.Databases
{
    using System;
    using System.Collections.Generic;
    
    public partial class ЭлементПланировка
    {
        public int КодПланировки { get; set; }
        public int КодЭлемента { get; set; }
        public int КоординатаX { get; set; }
        public int КоординатаY { get; set; }
    
        public virtual Планировка Планировка { get; set; }
        public virtual Элемент Элемент { get; set; }
    }
}
