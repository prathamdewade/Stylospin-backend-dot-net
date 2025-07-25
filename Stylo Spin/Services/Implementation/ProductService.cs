using Stylo_Spin.Dtos;
using Stylo_Spin.Helper;
using Stylo_Spin.Models;
using Stylo_Spin.Repository.Defination;
using Stylo_Spin.Services.Defination;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repo, ICategoryRepository categoryRepo, ILogger<ProductService> logger)
    {
        _repo = repo;
        _categoryRepo = categoryRepo;
        _logger = logger;
    }

    public async Task<TblProduct> CreateProductAsync(ProductDto dto)
    {
        _logger.LogInformation("Creating product: {ProductName}", dto.PName);

        var category = await _categoryRepo.GetCategoryByNameAsync(dto.C_Name);

        if (category == null)
        {
            category = new TblCategory { CName = dto.C_Name, Status = true };
            bool categoryCreated = await _categoryRepo.AddCategoryAsync(category);

            if (!categoryCreated)
            {
                _logger.LogError("Failed to create category: {CategoryName}", dto.C_Name);
                throw new Exception($"Failed to create category '{dto.C_Name}'");
            }

            // If AddCategoryAsync() returns the entity, no need to fetch again.
            category = await _categoryRepo.GetCategoryByNameAsync(dto.C_Name);

            if (category == null)
            {
                _logger.LogError("Category not found after creation: {CategoryName}", dto.C_Name);
                throw new Exception($"Category '{dto.C_Name}' could not be retrieved after creation.");
            }
        }

        byte[] imageData = await ImageHelper.ConvertToBytesAsync(dto.Image);

        var product = new TblProduct
        {
            PName = dto.PName,
            Status = dto.Status,
            Description = dto.Description,
            ImageData = imageData,
            ImageName = dto.Image?.FileName,
            Price = dto.Price,
            CId = category.CId,
            ProductQuantity = dto.ProductQuantity
        };

        var result = await _repo.AddProduct(product);

        if (result)
        {
            _logger.LogInformation("Product created successfully: {ProductName}", product.PName);
            return product;
        }

        _logger.LogError("Failed to add product: {ProductName}", dto.PName);
        throw new Exception("Failed to add product.");
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        _logger.LogInformation("Deleting product with ID: {ProductId}", id);

        var product = await _repo.GetProductById(id);
        if (product == null)
        {
            _logger.LogWarning("Product not found for deletion: ID = {ProductId}", id);
            return false;
        }

        var result = await _repo.DeleteProduct(product);
        _logger.LogInformation("Product deletion {Status} for ID: {ProductId}", result ? "succeeded" : "failed", id);
        return result;
    }

    public async Task<List<TblProduct>> GetAllProductsAsync()
    {
        _logger.LogInformation("Retrieving all products");
        return await _repo.GetAllProducts();
    }

    public async Task<TblProduct> GetProductByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving product by ID: {ProductId}", id);
        var product = await _repo.GetProductById(id);
        if (product == null)
        {
            _logger.LogWarning("Product not found with ID: {ProductId}", id);
            throw new KeyNotFoundException("Product not found");
        }

        return product;
    }

    public async Task<List<TblProduct>> GetProductsByCategoryAsync(int categoryId)
    {
        _logger.LogInformation("Retrieving products by category ID: {CategoryId}", categoryId);
        var allProducts = await _repo.GetAllProducts();
        return allProducts.Where(p => p.CId == categoryId).ToList();
    }

    public async Task<List<TblProduct>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        _logger.LogInformation("Retrieving products in price range: {MinPrice} - {MaxPrice}", minPrice, maxPrice);
        var allProducts = await _repo.GetAllProducts();
        return allProducts.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
    }

    public async Task<List<TblProduct>> SearchProductsAsync(string searchTerm)
    {
        _logger.LogInformation("Searching products with term: {SearchTerm}", searchTerm);
        var allProducts = await _repo.GetAllProducts();
        return allProducts.Where(p => p.PName != null && p.PName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public async Task<TblProduct> UpdateProductAsync(int id, ProductDto dto)
    {
        _logger.LogInformation("Updating product ID: {ProductId}", id);

        var product = await _repo.GetProductById(id);
        if (product == null)
        {
            _logger.LogWarning("Product not found for update: ID = {ProductId}", id);
            throw new KeyNotFoundException("Product not found");
        }

        var category = await _categoryRepo.GetCategoryByNameAsync(dto.C_Name);
        if (category == null)
        {
            _logger.LogWarning("Category not found: {Category}", dto.C_Name);
            throw new Exception($"Category '{dto.C_Name}' not found");
        }

        byte[] imageData = await ImageHelper.ConvertToBytesAsync(dto.Image);

        product.PName = dto.PName;
        product.Status = dto.Status;
        product.Description = dto.Description;
        product.ImageData = imageData ?? product.ImageData;
        product.ImageName = dto.Image?.FileName ?? product.ImageName;
        product.Price = dto.Price;
        product.CId = category.CId;
        product.ProductQuantity = dto.ProductQuantity;

        var result = await _repo.UpdateProduct(product)??false;
        if (result)
        {
            _logger.LogInformation("Product updated successfully: ID = {ProductId}", id);
            return product;
        }

        _logger.LogError("Failed to update product: ID = {ProductId}", id);
        throw new Exception("Failed to update product");
    }
}
