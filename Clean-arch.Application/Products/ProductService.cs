﻿using Clean_arch.Application.Products.DTOs;
using Clean_arch.Domain.Products;

namespace Clean_arch.Application.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public void AddProduct(AddProductDto command)
    {
        _repository.Add(new Product(command.Title, command.Price));
        _repository.Save();
    }

    public void EditProduct(EditProductDto command)
    {
        var product = _repository.GetById(command.Id);
        product.Edit(command.Title, command.Price);

        _repository.Update(product);
        _repository.Save();
    }

    public ProductDto GetProductById(Guid productId)
    {
        var product = _repository.GetById(productId);
        return new ProductDto()
        {
            Price = product.Price,
            Id = productId,
            Title = product.Title
        };
    }

    public List<ProductDto> GetProducts()
    {
        return _repository.GetList().Select(product => new ProductDto()
        {
            Price = product.Price,
            Id = product.Id,
            Title = product.Title
        }).ToList();

    }
}