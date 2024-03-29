﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GroceryStoreDatabasesEntities : DbContext
    {
        public GroceryStoreDatabasesEntities()
            : base("name=GroceryStoreDatabasesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Адрес> Адрес { get; set; }
        public virtual DbSet<Аккаунт> Аккаунт { get; set; }
        public virtual DbSet<Должность> Должность { get; set; }
        public virtual DbSet<ЕдиницаИзмерения> ЕдиницаИзмерения { get; set; }
        public virtual DbSet<КатегорияТовара> КатегорияТовара { get; set; }
        public virtual DbSet<МестоОтправки> МестоОтправки { get; set; }
        public virtual DbSet<МодификаторЦены> МодификаторЦены { get; set; }
        public virtual DbSet<НаселённыйПункт> НаселённыйПункт { get; set; }
        public virtual DbSet<Планировка> Планировка { get; set; }
        public virtual DbSet<Поставка> Поставка { get; set; }
        public virtual DbSet<Поставщик> Поставщик { get; set; }
        public virtual DbSet<Регион> Регион { get; set; }
        public virtual DbSet<Склад> Склад { get; set; }
        public virtual DbSet<Сотрудник> Сотрудник { get; set; }
        public virtual DbSet<Списание> Списание { get; set; }
        public virtual DbSet<СписаниеТовар> СписаниеТовар { get; set; }
        public virtual DbSet<Стеллаж> Стеллаж { get; set; }
        public virtual DbSet<Тип> Тип { get; set; }
        public virtual DbSet<ТипСтелажа> ТипСтелажа { get; set; }
        public virtual DbSet<Товар> Товар { get; set; }
        public virtual DbSet<ТоварПоставка> ТоварПоставка { get; set; }
        public virtual DbSet<ТорговыйЗал> ТорговыйЗал { get; set; }
        public virtual DbSet<ТорговыйЗалСтеллаж> ТорговыйЗалСтеллаж { get; set; }
        public virtual DbSet<Улица> Улица { get; set; }
        public virtual DbSet<Филиал> Филиал { get; set; }
        public virtual DbSet<ФилиалТовар> ФилиалТовар { get; set; }
        public virtual DbSet<Элемент> Элемент { get; set; }
        public virtual DbSet<ЭлементПланировка> ЭлементПланировка { get; set; }
    }
}
