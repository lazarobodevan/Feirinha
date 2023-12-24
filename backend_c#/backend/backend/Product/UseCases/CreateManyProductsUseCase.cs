﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Product.DTOs;
using backend.Product.Enums;
using backend.Product.Repository;
using backend.Utils;

namespace backend.Product.UseCases;

public class CreateManyProductsUseCase{
    private readonly IProductRepository repository;

    public CreateManyProductsUseCase(IProductRepository repository){
        this.repository = repository;
    }

    public async Task<IEnumerable<Models.Product>> Execute(CreateProductDTO[] productsDTO){
        DateTime parsedDateTime;

        List<Models.Product> productsEntities = new List<Models.Product>();
        foreach (var product in productsDTO){
            parsedDateTime = DateUtils.ConvertStringToDateTime(product.HarvestDate!, "dd/MM/yyyy");

            var p = new Models.Product{
                Name = product.Name,
                Description = product.Description,
                Picture = product.Picture,
                Category = (Category)product.Category!,
                Price = (double)product.Price!,
                Unit = (Unit)product.Unit!,
                AvailableQuantity = (int)product.AvailableQuantity!,
                IsOrganic = (bool)product.IsOrganic!,
                HarvestDate = parsedDateTime,
                ProducerId = (Guid)product.ProducerId!
            };
            productsEntities.Add(p);
        }

        return await repository.SaveMany(productsEntities.ToArray());
    }
}