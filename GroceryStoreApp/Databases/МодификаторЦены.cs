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
    
    public partial class МодификаторЦены
    {
        public int Код { get; set; }
        public byte Значение { get; set; }
        public bool Функция { get; set; }
        public int КодФилиала { get; set; }
        public int КодТовара { get; set; }
    
        public virtual Товар Товар { get; set; }
        public virtual Филиал Филиал { get; set; }
    }
}
