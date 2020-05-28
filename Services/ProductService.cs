using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Domain.Services.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
       
        public async Task<ProductResponse> DeleteAsync(Product product)
        {
            if (_productRepository.GetById(product.ID) == null)
                return new ProductResponse("Object With Specific ID Was Null");

            try
            {
                _productRepository.Delete(product);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when deleting the Product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> DeleteByIDAsync(int id)
        {
            var existingProduct = await _productRepository.GetById(id);

            if (existingProduct == null)
                return new ProductResponse("Was not found.");

            try
            {
                _productRepository.Delete(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when deleting the Product: {ex.Message}");
            }
        }

        public async Task<Product> GetById(int id)
        {
            if (id != 0)
                return
                    await _productRepository.GetById(id);

            return null;
        }

        public async Task<ProductResponse> InsertAsync(Product product)
        {
            try
            {
                var existingProduct = await _productRepository.GetById(product.ID);
                if (existingProduct != null)
                    return new ProductResponse("Invalid product.");

                await _productRepository.Insert(product);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when saving the Product: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            var products = await _productRepository.GetAll();

            return products;
        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _productRepository.GetById(id);

            if (existingProduct == null)
                return new ProductResponse("was not found.");

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.ProductCode = product.ProductCode;

            try
            {
                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when updating the Product: {ex.Message}");
            }
        }
    }
}
