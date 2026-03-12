using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Product.API.Models;

namespace Product.API.Data;

public partial class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Models.Product> Product { get; set; }

    public virtual DbSet<ProductDescription> ProductDescription { get; set; }

    public virtual DbSet<ProductDetail> ProductDetail { get; set; }

    public virtual DbSet<ProductDocument> ProductDocument { get; set; }

    public virtual DbSet<ProductDocumentMailing> ProductDocumentMailing { get; set; }

    public virtual DbSet<ProductDocumentType> ProductDocumentType { get; set; }

    public virtual DbSet<ProductGroup> ProductGroup { get; set; }

    public virtual DbSet<ProductGroupDescription> ProductGroupDescription { get; set; }

    public virtual DbSet<ProductGroupDetail> ProductGroupDetail { get; set; }

    public virtual DbSet<ProductGroupLifeCycleStatus> ProductGroupLifeCycleStatus { get; set; }

    public virtual DbSet<ProductGroupToProduct> ProductGroupToProduct { get; set; }

    public virtual DbSet<ProductLifeCycleStatus> ProductLifeCycleStatus { get; set; }

    public virtual DbSet<ProductRoleAllowed> ProductRoleAllowed { get; set; }

    public virtual DbSet<ProductToCondition> ProductToCondition { get; set; }

    public virtual DbSet<ProductToProduct> ProductToProduct { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AI");

        modelBuilder.Entity<Models.Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasFillFactor(90);

            entity.ToTable("Product", "product");

            entity.HasIndex(e => new { e.ProductCode, e.ProductId, e.GroupBankId }, "IX_Product");

            entity.HasIndex(e => new { e.ProductCode, e.GroupBankId }, "IX_Product_UniqueLogicalKey").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.ProductCode).HasMaxLength(8);
        });

        modelBuilder.Entity<ProductDescription>(entity =>
        {
            entity.HasKey(e => e.ProductDescriptionId).HasFillFactor(90);

            entity.ToTable("ProductDescription", "product");

            entity.HasIndex(e => new { e.ProductId, e.LanguageId, e.ValidEndDate }, "IX_ProductDescription_UniqueLogicalKey")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.ProductDescriptionId).HasColumnName("ProductDescriptionID");
            entity.Property(e => e.Description).HasMaxLength(40);
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductDescription_ValidEndDate");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDescription)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductDescriptionProductID_ProductProductID");
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasKey(e => e.ProductDetailId).HasFillFactor(90);

            entity.ToTable("ProductDetail", "product");

            entity.HasIndex(e => new { e.ProductId, e.GroupBankId }, "IX_ProductDetail_ProductID");

            entity.HasIndex(e => new { e.ProductId, e.ValidEndDate }, "IX_ProductDetail_UniqueLogicalKey").IsUnique();

            entity.Property(e => e.ProductDetailId).HasColumnName("ProductDetailID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.Molteplicity).HasDefaultValue(false);
            entity.Property(e => e.PltAreaId).HasColumnName("PltAreaID");
            entity.Property(e => e.PltClassId).HasColumnName("PltClassID");
            entity.Property(e => e.PltFamilyId).HasColumnName("PltFamilyID");
            entity.Property(e => e.PltMarketId).HasColumnName("PltMarketID");
            entity.Property(e => e.PltStructureId).HasColumnName("PltStructureID");
            entity.Property(e => e.PltSubclassId).HasColumnName("PltSubclassID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductDetail_ValidEndDate");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDetail)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductDetailProductID_ProductProductID");
        });

        modelBuilder.Entity<ProductDocument>(entity =>
        {
            entity.ToTable("ProductDocument", "product");

            entity.Property(e => e.ProductDocumentId).HasColumnName("ProductDocumentID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.PltArchiveId).HasColumnName("PltArchiveID");
            entity.Property(e => e.PltFormId).HasColumnName("PltFormID");
            entity.Property(e => e.PltFormUseCaseId).HasColumnName("PltFormUseCaseID");
            entity.Property(e => e.PltOptionId).HasColumnName("PltOptionID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UseCaseGridRowCommand)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductDocument_ValidEndDate");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDocument)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductDocumentProductID_ProductProductID");
        });

        modelBuilder.Entity<ProductDocumentMailing>(entity =>
        {
            entity.ToTable("ProductDocumentMailing", "product");

            entity.HasIndex(e => e.PltMailingDocumentTypeId, "IX_ProductDocumentMailing1");

            entity.HasIndex(e => e.GroupBankId, "IX_ProductDocumentMailing2");

            entity.HasIndex(e => e.ProductId, "IX_ProductDocumentMailing3");

            entity.HasIndex(e => new { e.ProductId, e.PltMailingDocumentTypeId }, "IX_ProductDocumentMailing_Unique")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.ProductDocumentMailingId).HasColumnName("ProductDocumentMailingID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.PltMailingDocumentTypeId).HasColumnName("PltMailingDocumentTypeID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductDocumentMailing_ValidEndDate");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDocumentMailing)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product__ProductDocumentMailing");
        });

        modelBuilder.Entity<ProductDocumentType>(entity =>
        {
            entity.ToTable("ProductDocumentType", "product");

            entity.Property(e => e.ProductDocumentTypeId).HasColumnName("ProductDocumentTypeID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.PltDocumentTypeId).HasColumnName("PltDocumentTypeID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductDocumentType_ValidEndDate");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDocumentType)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductDocumentTypeProductID_ProductProductID");
        });

        modelBuilder.Entity<ProductGroup>(entity =>
        {
            entity.ToTable("ProductGroup", "product");

            entity.HasIndex(e => new { e.ProductGroupCode, e.GroupBankId }, "IX_ProductGroup_UniqueLogicalKey").IsUnique();

            entity.Property(e => e.ProductGroupId).HasColumnName("ProductGroupID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.ProductGroupCode).HasMaxLength(8);
        });

        modelBuilder.Entity<ProductGroupDescription>(entity =>
        {
            entity.ToTable("ProductGroupDescription", "product");

            entity.Property(e => e.ProductGroupDescriptionId).HasColumnName("ProductGroupDescriptionID");
            entity.Property(e => e.Description).HasMaxLength(40);
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.ProductGroupId).HasColumnName("ProductGroupID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductGroupDescription_ValidEndDate");

            entity.HasOne(d => d.ProductGroup).WithMany(p => p.ProductGroupDescription)
                .HasForeignKey(d => d.ProductGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductGroupDescriptionProductGroupID_ProductGroupProductGroupID");
        });

        modelBuilder.Entity<ProductGroupDetail>(entity =>
        {
            entity.ToTable("ProductGroupDetail", "product");

            entity.HasIndex(e => new { e.ProductGroupId, e.ValidEndDate }, "IX_ProductGroupDetail_UniqueLogicalKey").IsUnique();

            entity.Property(e => e.ProductGroupDetailId).HasColumnName("ProductGroupDetailID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.PltGroupTypeId).HasColumnName("PltGroupTypeID");
            entity.Property(e => e.ProductGroupId).HasColumnName("ProductGroupID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductGroup_ValidEndDate");

            entity.HasOne(d => d.ProductGroup).WithMany(p => p.ProductGroupDetail)
                .HasForeignKey(d => d.ProductGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductGroupDetailProductGroupID_ProductGroupProductGroupID");
        });

        modelBuilder.Entity<ProductGroupLifeCycleStatus>(entity =>
        {
            entity.ToTable("ProductGroupLifeCycleStatus", "product");

            entity.Property(e => e.ProductGroupLifeCycleStatusId).HasColumnName("ProductGroupLifeCycleStatusID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.PltProductGroupStatusId).HasColumnName("PltProductGroupStatusID");
            entity.Property(e => e.ProductGroupId).HasColumnName("ProductGroupID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductGroupLifeCycleStatus_ValidEndDate");

            entity.HasOne(d => d.ProductGroup).WithMany(p => p.ProductGroupLifeCycleStatus)
                .HasForeignKey(d => d.ProductGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductGroupLifeCycleStatusProductGroupID_ProductGroupProductGroupID");
        });

        modelBuilder.Entity<ProductGroupToProduct>(entity =>
        {
            entity.ToTable("ProductGroupToProduct", "product");

            entity.Property(e => e.ProductGroupToProductId).HasColumnName("ProductGroupToProductID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.ProductGroupId).HasColumnName("ProductGroupID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductGroupToProduct_ValidEndDate");

            entity.HasOne(d => d.ProductGroup).WithMany(p => p.ProductGroupToProduct)
                .HasForeignKey(d => d.ProductGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductGroupToProductGroupProductGroupID_ProductGroupProductGroupID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductGroupToProduct)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductGroupToProductProductID_ProductProductID");
        });

        modelBuilder.Entity<ProductLifeCycleStatus>(entity =>
        {
            entity.HasKey(e => e.ProductLifeCycleStatusId).HasFillFactor(90);

            entity.ToTable("ProductLifeCycleStatus", "product");

            entity.Property(e => e.ProductLifeCycleStatusId).HasColumnName("ProductLifeCycleStatusID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.PltProductStatusId).HasColumnName("PltProductStatusID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductLifeCycleStatus_ValidEndDate");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductLifeCycleStatus)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductLifeCycleStatusProductID_ProductProductID");
        });

        modelBuilder.Entity<ProductRoleAllowed>(entity =>
        {
            entity.ToTable("ProductRoleAllowed", "product");

            entity.Property(e => e.ProductRoleAllowedId).HasColumnName("ProductRoleAllowedID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.Molteplicity).HasDefaultValue(false);
            entity.Property(e => e.PltRoleAllowedId).HasColumnName("PltRoleAllowedID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductRoleAllowed_ValidEndDate");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductRoleAllowed)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductRoleAllowedProductID_ProductProductID");
        });

        modelBuilder.Entity<ProductToCondition>(entity =>
        {
            entity.HasKey(e => e.ProductToConditionId).HasFillFactor(90);

            entity.ToTable("ProductToCondition", "product");

            entity.HasIndex(e => new { e.ConditionId, e.ValidStartDate, e.ValidEndDate }, "IX_ProductToCondition_Condition_Validity");

            entity.HasIndex(e => e.ProductId, "IX_ProductToCondition_ProductID").HasFillFactor(90);

            entity.Property(e => e.ProductToConditionId).HasColumnName("ProductToConditionID");
            entity.Property(e => e.BusinessEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductToCondition_BusinessEndDate");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.ConditionId).HasColumnName("ConditionID");
            entity.Property(e => e.ConditionValueId).HasColumnName("ConditionValueID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.PltApplicabilityId).HasColumnName("PltApplicabilityID");
            entity.Property(e => e.PltObligatorinessId).HasColumnName("PltObligatorinessID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductToCondition_ValidEndDate");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductToCondition)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductToConditionProductID_ProductProductID");
        });

        modelBuilder.Entity<ProductToProduct>(entity =>
        {
            entity.HasKey(e => e.ProductToProductId).HasFillFactor(90);

            entity.ToTable("ProductToProduct", "product");

            entity.Property(e => e.ProductToProductId).HasColumnName("ProductToProductID");
            entity.Property(e => e.GroupBankId).HasColumnName("GroupBankID");
            entity.Property(e => e.PltAssetToAssetLinkId).HasColumnName("PltAssetToAssetLinkID");
            entity.Property(e => e.PltModeId).HasColumnName("PltModeID");
            entity.Property(e => e.PltRoleId).HasColumnName("PltRoleID");
            entity.Property(e => e.ProductChildId).HasColumnName("ProductChildID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ValidEndDate).HasDefaultValueSql("([dbo].[udf_GetMaxDateTime2]())", "DF_ProductToProduct_ValidEndDate");

            entity.HasOne(d => d.ParentProduct).WithMany(p => p.ProductToProductParentProduct)
                .HasForeignKey(d => d.ParentProductId)
                .HasConstraintName("FK_ProductToProduct_Product");

            entity.HasOne(d => d.ProductChild).WithMany(p => p.ProductToProductProductChild)
                .HasForeignKey(d => d.ProductChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductToProductProductChildID_ProductProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductToProductProduct)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductToProductProductID_ProductProductID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
