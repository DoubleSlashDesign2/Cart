﻿using CartWeb.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartWeb.Data
{
    public class CartContext : IdentityDbContext<StoreUser>
    {
        public CartContext(DbContextOptions<CartContext> options): base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
              .Property(p => p.Price)
              .HasColumnType("decimal(18,2)");

            builder.Entity<OrderItem>()
              .Property(p => p.UnitPrice)
              .HasColumnType("decimal(18,2)");
        }
    }
}
