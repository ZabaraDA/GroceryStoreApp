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
    
    public partial class Филиал
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Филиал()
        {
            this.МодификаторЦены = new HashSet<МодификаторЦены>();
            this.Поставка = new HashSet<Поставка>();
            this.Списание = new HashSet<Списание>();
            this.Стеллаж = new HashSet<Стеллаж>();
            this.ТорговыйЗал = new HashSet<ТорговыйЗал>();
            this.ФилиалТовар = new HashSet<ФилиалТовар>();
            this.Сотрудник = new HashSet<Сотрудник>();
        }
    
        public int Код { get; set; }
        public int Адрес { get; set; }
        public string Наименование { get; set; }
    
        public virtual Адрес Адрес1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<МодификаторЦены> МодификаторЦены { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Поставка> Поставка { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Списание> Списание { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Стеллаж> Стеллаж { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ТорговыйЗал> ТорговыйЗал { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ФилиалТовар> ФилиалТовар { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Сотрудник> Сотрудник { get; set; }
    }
}
