using Blazor.Server.UI.Models.SideMenu;
using MudBlazor;

namespace Blazor.Server.UI.Services.Navigation;

public class MenuService : IMenuService
{
    private readonly List<MenuSectionModel> _features = new List<MenuSectionModel>()
    {
        new MenuSectionModel
        {
            Title = "Application",
            SectionItems = new List<MenuSectionItemModel>
            {
                new()
                {
                    Title = "Home",
                    Icon = Icons.Material.Filled.Dashboard,
                    Href = "/"
                },
                new()
                {
                    Title = "Transportation",
                    Icon = Icons.Material.Filled.AirplaneTicket,
                    PageStatus = PageStatus.Completed,
                    IsParent = true,
                    MenuItems = new List<MenuSectionSubItemModel>
                    {
                        new(){
                             Title = "Shipping Orders",
                             Href = "/tms/shippingorders",
                             PageStatus = PageStatus.Completed,
                        }
                    }
                },
               new()
                {
                    Title = "Trucks",
                    Icon = Icons.Filled.LocalShipping,
                    PageStatus = PageStatus.Completed,
                    IsParent = true,
                    MenuItems = new List<MenuSectionSubItemModel>
                    {
                        new(){
                             Title = "Trucks",
                             Href = "/tms/trucks",
                             PageStatus = PageStatus.Completed,
                        }

                    }
                },
            }
        },

        new MenuSectionModel
        {
            Title = "MANAGEMENT",
            SectionItems = new List<MenuSectionItemModel>
            {
                new()
                {
                    IsParent = true,
                    Title = "Authorization",
                    Icon = Icons.Material.Filled.Person,
                    MenuItems = new List<MenuSectionSubItemModel>
                    {
                        new()
                        {
                            Title = "Users",
                            Href = "/indentity/users",
                            PageStatus = PageStatus.Completed
                        },
                        new()
                        {
                            Title = "Roles",
                            Href = "/indentity/roles",
                            PageStatus = PageStatus.Completed
                        },
                        new()
                        {
                            Title = "Profile",
                            Href = "/user/profile",
                            PageStatus = PageStatus.Completed
                        }
                    }
                },
                new()
                {
                    IsParent = true,
                    Title = "System",
                    Icon = Icons.Material.Filled.Devices,
                    MenuItems = new List<MenuSectionSubItemModel>
                    {   new()
                        {
                            Title = "Dictionaries",
                            Href = "/system/dictionaries",
                            PageStatus = PageStatus.Completed
                        },
                        new()
                        {
                            Title = "Audit Trails",
                            Href = "/system/audittrails",
                            PageStatus = PageStatus.Completed
                        },
                        new()
                        {
                            Title = "Log",
                            Href = "/system/logs",
                            PageStatus = PageStatus.Completed
                        },
                        new()
                        {
                            Title = "Jobs",
                            Href = "/hangfire/index",
                            PageStatus = PageStatus.ComingSoon
                        }
                    }
                }

            }
        }
    };

    public IEnumerable<MenuSectionModel> Features => _features;
}
