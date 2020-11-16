using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.Entities;
using ClubAdministration.Persistence.Validations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly ApplicationDbContext _dbContext;
    private bool _disposed;
    private readonly DuplicateMemberValidation _duplicateValidation;

    public UnitOfWork()
    {
      _dbContext = new ApplicationDbContext();
      MemberRepository = new MemberRepository(_dbContext);
      SectionRepository = new SectionRepository(_dbContext);
      MemberSectionRepository = new MemberSectionRepository(_dbContext);
      _duplicateValidation = new DuplicateMemberValidation(this);
    }

    public IMemberRepository MemberRepository { get; }
    public ISectionRepository SectionRepository { get; }
    public IMemberSectionRepository MemberSectionRepository { get; }


    /// <summary>
    /// Repository-übergreifendes Speichern der Änderungen
    /// </summary>
    public async Task<int> SaveChangesAsync()
    {
      var entities = _dbContext.ChangeTracker.Entries()
          .Where(entity => entity.State == EntityState.Added
                           || entity.State == EntityState.Modified)
          .Select(e => e.Entity);

      foreach (var entity in entities)
      {
        ValidateEntity(entity);
      }

      return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposed)
      {
        if (disposing)
        {
          _dbContext.Dispose();
        }
      }
      _disposed = true;
    }
    public async Task DeleteDatabaseAsync() => await _dbContext.Database.EnsureDeletedAsync();
    public async Task MigrateDatabaseAsync() => await _dbContext.Database.MigrateAsync();

    private void ValidateEntity(object entity)
    {
      // Validierung der hinterlegten Validierungsattribute
      Validator.ValidateObject(entity, new ValidationContext(entity), true);
      switch (entity)
      {
        case Member member:
          ValidationResult result = _duplicateValidation.GetValidationResult(member, new ValidationContext(member));
          if (result != null && result != ValidationResult.Success)
          {
            throw new ValidationException(result, _duplicateValidation, member);
          }
          break;
        default:
          return;
      }
    }

  }
}
