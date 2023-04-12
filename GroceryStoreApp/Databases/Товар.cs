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
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public partial class Товар :INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Товар()
        {
            this.ЗаказТовар = new HashSet<ЗаказТовар>();
            this.МодификаторЦены = new HashSet<МодификаторЦены>();
            this.СписаниеТовар = new HashSet<СписаниеТовар>();
            this.ТоварПоставка = new HashSet<ТоварПоставка>();
            this.ФилиалТовар = new HashSet<ФилиалТовар>();
            this.Склад = new HashSet<Склад>();
        }
    
        public int Код { get; set; }
        public string Артикул { get; set; }
        public string ШтрихКод { get; set; }
        public string Наименование { get; set; }
        public string Описание { get; set; }
        public int КодПроизводителя { get; set; }
        public int КодКатегории { get; set; }
        public decimal Цена { get; set; }
        public byte НДС { get; set; }
        public decimal Количество { get; set; }
        public int СрокГодности { get; set; }
        public int СрокОповещения { get; set; }
        public int КодЕдиницыИзмерения { get; set; }

        private byte[] фото { get; set; }
        public byte[] Фото
        {
            get { return фото; }
            set
            {
                фото = value;
                OnPropertyChanged("Фото");
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public decimal Вес { get; set; }
        public int Высота { get; set; }
        public int Ширина { get; set; }
        public int Глубина { get; set; }
        public bool Статус { get; set; }
    
        public virtual ЕдиницаИзмерения ЕдиницаИзмерения { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ЗаказТовар> ЗаказТовар { get; set; }
        public virtual Категория Категория { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<МодификаторЦены> МодификаторЦены { get; set; }
        public virtual Производитель Производитель { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<СписаниеТовар> СписаниеТовар { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ТоварПоставка> ТоварПоставка { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ФилиалТовар> ФилиалТовар { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Склад> Склад { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
