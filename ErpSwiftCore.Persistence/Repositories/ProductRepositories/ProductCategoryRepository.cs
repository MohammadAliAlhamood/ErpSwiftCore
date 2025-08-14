using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories.IProductRepositories; 
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces; 
using System.Linq.Expressions; 
namespace ErpSwiftCore.Persistence.Repositories.ProductRepositories
{ 
    public class ProductCategoryRepository : MultiTenantRepository<ProductCategory>, IProductCategoryRepository
    {
        private static readonly Expression<Func<ProductCategory, object>>[] DefaultIncludes =
        {  x => x.ParentCategory };

        public ProductCategoryRepository(AppDbContext db, ITenantProvider tenantProvider, 
            IUserProvider userProvider, IIncludeValidator<ProductCategory> includeValidator) 
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }





        // ----------- [CRUD & Bulk] -----------
        public async Task<Guid> CreateAsync(ProductCategory category, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(category, true, cancellationToken);
            return category.ID;
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<ProductCategory> categories, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var c in categories)
                ids.Add(await CreateAsync(c, cancellationToken));
            return ids;
        }

        public async Task<int> BulkImportAsync(IEnumerable<ProductCategory> categories, CancellationToken cancellationToken = default)
        {
            await base.AddRangeAsync(categories, true, cancellationToken);
            return categories.Count();
        }

        public async Task<bool> UpdateAsync(ProductCategory category, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(category, true, cancellationToken);
            return true;
        }

        // ----------- [Delete/Archive/Restore] -----------
        public async Task<bool> DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(c => c.ID == categoryId, cancellationToken: cancellationToken);
            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in categoryIds)
                ok &= await DeleteAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            return await DeleteRangeAsync(all.Select(c => c.ID), cancellationToken);
        }

        public async Task<bool> RestoreAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(c => c.ID == categoryId, cancellationToken);
            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in categoryIds)
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllSoftDeletedAsync(cancellationToken);
            return await RestoreRangeAsync(all.Select(c => c.ID), cancellationToken);
        }

        
        // ----------- [Get/Query - Single] -----------
        public Task<ProductCategory?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => base.GetAsync(c => c.ID == categoryId, cancellationToken, DefaultIncludes);

        public Task<ProductCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
            => base.GetAsync(c => c.Name == name, cancellationToken, DefaultIncludes);

        public async Task<ProductCategory?> GetSoftDeletedByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => await base.GetOneSoftDeletedAsync(c => c.ID == categoryId, cancellationToken, DefaultIncludes);

        // ----------- [Get/Query - Bulk/Advanced] -----------
        public async Task<IReadOnlyList<ProductCategory>> GetAllAsync(CancellationToken cancellationToken = default)
            => await base.GetAllAsync(null, cancellationToken, DefaultIncludes);

        public async Task<IReadOnlyList<ProductCategory>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default)
            => await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);

        
        public async Task<IReadOnlyList<ProductCategory>> GetByParentAsync(Guid parentCategoryId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(c => c.ParentCategoryId == parentCategoryId, cancellationToken, DefaultIncludes);

        public async Task<IReadOnlyList<ProductCategory>> GetByIdsAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(c => categoryIds.Contains(c.ID), cancellationToken, DefaultIncludes);

        public async Task<IReadOnlyList<ProductCategory>> GetByFilterAsync(Expression<Func<ProductCategory, bool>> filter, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);

        // ----------- [Hierarchy/Descendants] -----------
        public async Task<IReadOnlyList<ProductCategory>> GetAllDescendantsAsync(Guid parentCategoryId, CancellationToken cancellationToken = default)
        {
            var result = new List<ProductCategory>();
            var directChildren = await GetByParentAsync(parentCategoryId, cancellationToken);
            foreach (var child in directChildren)
            {
                result.Add(child);
                var descendants = await GetAllDescendantsAsync(child.ID, cancellationToken);
                result.AddRange(descendants);
            }
            return result ;
        }

        public async Task<IReadOnlyList<ProductCategory>> GetCategoryHierarchyAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var hierarchy = new List<ProductCategory>();
            var current = await GetByIdAsync(categoryId, cancellationToken);
            while (current != null)
            {
                hierarchy.Insert(0, current);
                if (current.ParentCategoryId == null)
                    break;
                current = await GetByIdAsync(current.ParentCategoryId.Value, cancellationToken);
            }
            return hierarchy ;
        }

                    // ----------- [Existence & Validation] -----------
        public Task<bool> ExistsByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(c => c.ID == categoryId, cancellationToken);

        public Task<bool> ExistsByNameAsync(string name, Guid? parentCategoryId = null, Guid? excludeCategoryId = null, CancellationToken cancellationToken = default)
        {
            return base.ExistsAsync(
                c => c.Name == name
                    && (!parentCategoryId.HasValue || c.ParentCategoryId == parentCategoryId)
                    && (!excludeCategoryId.HasValue || c.ID != excludeCategoryId.Value),
                cancellationToken);
        }

        public async Task<bool> IsNameUniqueAsync(string name, Guid? parentCategoryId = null, Guid? excludeCategoryId = null, CancellationToken cancellationToken = default)
        {
            return !await ExistsByNameAsync(name, parentCategoryId, excludeCategoryId, cancellationToken);
        }

        public async Task<bool> IsValidParentCategoryAsync(Guid? parentCategoryId, Guid? excludeCategoryId = null, CancellationToken cancellationToken = default)
        {
            if (!parentCategoryId.HasValue)
                return true;
            // Parent must exist and not be the same as the current category (to avoid self-parenting)
            var exists = await base.ExistsAsync(
                c => c.ID == parentCategoryId.Value && (!excludeCategoryId.HasValue || c.ID != excludeCategoryId.Value),
                cancellationToken);
            return exists;
        }

        public async Task<bool> ExistsCircularDependencyAsync(Guid categoryId, Guid? parentCategoryId, CancellationToken cancellationToken = default)
        {
            // Prevent circular dependency: check if parent is a descendant of the category
            if (!parentCategoryId.HasValue)
                return false;
            var descendants = await GetAllDescendantsAsync(categoryId, cancellationToken);
            return descendants.Any(d => d.ID == parentCategoryId.Value);
        }

        // ----------- [Relations: Parent/Sub/Hierarchy] -----------
        public Task<ProductCategory?> GetWithParentAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => base.GetAsync(c => c.ID == categoryId, cancellationToken, DefaultIncludes);

        public Task<ProductCategory?> GetWithSubCategoriesAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => base.GetAsync(c => c.ID == categoryId, cancellationToken, DefaultIncludes);

        // ----------- [Counts & Stats] -----------
        public Task<int> GetCountAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken);

        public async Task<int> GetCountByParentAsync(Guid parentCategoryId, bool recursive = false, CancellationToken cancellationToken = default)
        {
            if (!recursive)   return await base.CountAsync(c => c.ParentCategoryId == parentCategoryId, cancellationToken); 
            var descendants = await GetAllDescendantsAsync(parentCategoryId, cancellationToken);
            return descendants.Count;
        }

        // ----------- [Bulk Delete] -----------
        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default)
        {
            var categories = await GetByIdsAsync(categoryIds, cancellationToken);
            await base.SoftDeleteRangeAsync(categories, true, cancellationToken);
            return categories.Count;
        }

        public Task<bool> SoftDeleteAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ProductCategory>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}