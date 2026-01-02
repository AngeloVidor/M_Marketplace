using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.UseCases
{
    public class CreateProductUseCase
    {
        private readonly IProductRepository _repository;

        public CreateProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(CreateProductDto dto)
        {
            var product = new Product(
                dto.Name,
                dto.Description,
                new Money(dto.Price),
                new Stock(dto.Stock),
                dto.OwnerId
            );

            await _repository.AddAsync(product);
        }
    }
}
