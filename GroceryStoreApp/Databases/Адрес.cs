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
    
    public partial class Адрес
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Адрес()
        {
            this.МестоОтправки = new HashSet<МестоОтправки>();
            this.Сотрудник = new HashSet<Сотрудник>();
            this.Филиал = new HashSet<Филиал>();
        }
    
        public int Код { get; set; }
        public int КодРегиона { get; set; }
        public int КодНаселённогоПункта { get; set; }
        public int КодУлицы { get; set; }
        public short Дом { get; set; }
        public bool ТипАдреса { get; set; }
    
        public virtual НаселённыйПункт НаселённыйПункт { get; set; }
        public virtual Регион Регион { get; set; }
        public virtual Улица Улица { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<МестоОтправки> МестоОтправки { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Сотрудник> Сотрудник { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Филиал> Филиал { get; set; }
    }
}
