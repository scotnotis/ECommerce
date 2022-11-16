﻿using ECommerca.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerca.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();

        Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id);
    }
}
