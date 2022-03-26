// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Extensions;
using CleanArchitecture.Blazor.Infrastructure.Constants.Role;
using System.Reflection;


namespace CleanArchitecture.Blazor.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        var administratorRole = new ApplicationRole(RoleConstants.AdministratorRole) { Description = "Admin Group" };
        var userRole = new ApplicationRole(RoleConstants.BasicRole) { Description = "Basic Group" };

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
            await roleManager.CreateAsync(userRole);
            var Permissions = GetAllPermissions();
            foreach (var permission in Permissions)
            {
                await roleManager.AddClaimAsync(administratorRole, new System.Security.Claims.Claim(ApplicationClaimTypes.Permission, permission));
                if(permission.StartsWith("Permissions.Products"))
                  await roleManager.AddClaimAsync(userRole, new System.Security.Claims.Claim(ApplicationClaimTypes.Permission, permission));
            }
        }

        var administrator = new ApplicationUser { UserName = "administrator", IsActive = true, Site = "Razor", DisplayName = "Administrator", Email = "new163@163.com", EmailConfirmed = true, ProfilePictureDataUrl = $"https://s.gravatar.com/avatar/78be68221020124c23c665ac54e07074?s=80" };
        var demo = new ApplicationUser { UserName = "Demo", IsActive = true, Site = "Razor", DisplayName = "Demo", Email = "neozhu@126.com", EmailConfirmed = true, ProfilePictureDataUrl = $"https://s.gravatar.com/avatar/ea753b0b0f357a41491408307ade445e?s=80" };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, RoleConstants.DefaultPassword);
            await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            await userManager.CreateAsync(demo, RoleConstants.DefaultPassword);
            await userManager.AddToRolesAsync(demo, new[] { userRole.Name });
        }

    }
    private static IEnumerable<string> GetAllPermissions()
    {
        var allPermissions = new List<string>();
        var modules = typeof(Permissions).GetNestedTypes();

        foreach (var module in modules)
        {
            var moduleName = string.Empty;
            var moduleDescription = string.Empty;

            var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            foreach (var fi in fields)
            {
                var propertyValue = fi.GetValue(null);

                if (propertyValue is not null)
                    allPermissions.Add(propertyValue.ToString());
            }
        }

        return allPermissions;
    }

    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        //Seed, if necessary
        if (!context.DocumentTypes.Any())
        {
            context.DocumentTypes.Add(new Domain.Entities.DocumentType() { Name = "Document", Description = "Document" });
            context.DocumentTypes.Add(new Domain.Entities.DocumentType() { Name = "PDF", Description = "PDF" });
            context.DocumentTypes.Add(new Domain.Entities.DocumentType() { Name = "Image", Description = "Image" });
            context.DocumentTypes.Add(new Domain.Entities.DocumentType() { Name = "Other", Description = "Other" });
            await context.SaveChangesAsync();
         
        }
        if (!context.KeyValues.Any())
        {
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "VehicleType", Value = "厢式货车", Text = "厢式货车", Description = "车辆类型" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "VehicleType", Value = "平板式货车", Text = "平板式货车", Description = "车辆类型" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "VehicleType", Value = "自卸车", Text = "自卸车", Description = "车辆类型" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "VehicleType", Value = "重卡", Text = "重卡", Description = "车辆类型" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "VehicleType", Value = "两翼车", Text = "两翼车", Description = "车辆类型" });
            

            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "VehicleStatus", Value = "正常", Text = "正常", Description = "车辆状态" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "VehicleStatus", Value = "维修", Text = "维修", Description = "车辆状态" });

            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "ShippingOrderStatus", Value = "派单", Text = "派单", Description = "发运单状态" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "ShippingOrderStatus", Value = "发运", Text = "发运", Description = "发运单状态" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "ShippingOrderStatus", Value = "卸货完成", Text = "卸货完成", Description = "发运单状态" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "ShippingOrderStatus", Value = "关闭", Text = "关闭", Description = "发运单状态" });

            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "CostName", Value = "过路费", Text = "过路费", Description = "费用名称" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "CostName", Value = "燃油费", Text = "燃油费", Description = "费用名称" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "CostName", Value = "餐费", Text = "餐费", Description = "费用名称" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "CostName", Value = "装卸费", Text = "装卸费", Description = "费用名称" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "CostName", Value = "维修费", Text = "维修费", Description = "费用名称" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "CostName", Value = "违章罚款", Text = "违章罚款", Description = "费用名称" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "CostName", Value = "加水费", Text = "加水费", Description = "费用名称" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "CostName", Value = "其它费用", Text = "其它费用", Description = "费用名称" });
           


            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Status", Value = "initialization", Text = "initialization", Description = "Status of workflow" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Status", Value = "processing", Text = "processing", Description = "Status of workflow" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Status", Value = "pending", Text = "pending", Description = "Status of workflow" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Status", Value = "finished", Text = "finished", Description = "Status of workflow" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Brand", Value = "Apple", Text = "Apple", Description = "Brand of production" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Brand", Value = "MI", Text = "MI", Description = "Brand of production" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Brand", Value = "Logitech", Text = "Logitech", Description = "Brand of production" });
            context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Brand", Value = "Linksys", Text = "Linksys", Description = "Brand of production" });
            await context.SaveChangesAsync();
          
        }
        if (!context.Products.Any())
        {
            context.Products.Add(new Domain.Entities.Product() { Brand= "Apple", Name = "IPhone 13 Pro", Description= "Apple iPhone 13 Pro smartphone. Announced Sep 2021. Features 6.1″ display, Apple A15 Bionic chipset, 3095 mAh battery, 1024 GB storage.", Unit="EA",Price=999.98m });
            context.Products.Add(new Domain.Entities.Product() { Brand = "MI", Name = "MI 12 Pro", Description = "Xiaomi 12 Pro Android smartphone. Announced Dec 2021. Features 6.73″ display, Snapdragon 8 Gen 1 chipset, 4600 mAh battery, 256 GB storage.", Unit = "EA", Price = 199.00m });
            context.Products.Add(new Domain.Entities.Product() { Brand = "Logitech",  Name = "MX KEYS Mini", Description = "Logitech MX Keys Mini Introducing MX Keys Mini – a smaller, smarter, and mightier keyboard made for creators. Type with confidence on a keyboard crafted for efficiency, stability, and...", Unit = "PA", Price = 99.90m });
            await context.SaveChangesAsync();
        }

        if (!context.Trucks.Any())
        {
            context.Trucks.Add(new Domain.Entities.Truck() { PlateNumber = "京AM4Y871", VehicleType= "重卡", Status="正常",  Driver = "李荣", Owner="北京长途运输公司", PhoneNumber="13962698750", Description = "一汽解放J6P牵引车 460马力" });
            context.Trucks.Add(new Domain.Entities.Truck() { PlateNumber = "苏B9U3180", VehicleType = "厢式货车", Status = "正常", Driver = "王胜", Owner = "南京货物运输公司", PhoneNumber = "1597508971", Description = "东风商用车 天锦KR 245马力 4X2 6.8米排半栏板载货车(国六)(DFH1160E5)" });
            context.Trucks.Add(new Domain.Entities.Truck() { PlateNumber = "浙CMM9120", VehicleType = "两翼车", Status = "正常", Driver = "李兵", Owner = "杭州天天运输公司", PhoneNumber = "11262698750", Description = "江淮 帅铃Q8 绿巨人 154马力 5.2米排半仓栅式轻卡(HFC5141CCYP91K1C6V)" });
            await context.SaveChangesAsync();
        }
        if (!context.Drivers.Any())
        {
            context.Drivers.Add(new Domain.Entities.Driver() {Name="王师傅", PhoneNumber="1396266500", BrithDay=new DateTime(1972,1,15),Age=(DateTime.Now.Year - 1972), Address="上海市闵行区", IdentityNo="1234567890", DrivingNo="1234567890" , DrivingType="A", PayPeriod="月结", Remark="系统默认" });
            await context.SaveChangesAsync();
        }
    }
}
