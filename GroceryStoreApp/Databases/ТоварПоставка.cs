//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroceryStoreApp.Databases
{
    using System;
    using System.Collections.Generic;
    
    public partial class ТоварПоставка
    {
        public int КодПоставки { get; set; }
        public int КодТовара { get; set; }
        public int Количество { get; set; }
        public int Остаток { get; set; }
        public System.DateTime ДатаСписания { get; set; }
        public System.DateTime ДатаОповещения { get; set; }
    
        public virtual Поставка Поставка { get; set; }
        public virtual Товар Товар { get; set; }
    }
}
