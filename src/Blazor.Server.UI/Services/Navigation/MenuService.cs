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
                    Title = "Deshbord",
                    Icon = Icons.Material.Filled.Dashboard,
                    Href = "/"
                },
                new()
                {
                    Title = "Departments",
                    Icon = Icons.Material.Filled.Apartment,
                    Href = "/visitor/departments",
                    PageStatus = PageStatus.Completed
                },
                new()
                {
                    Title = "Designations",
                    Icon = Icons.Material.Filled.Fitbit,
                    Href = "/visitor/designations",
                    PageStatus = PageStatus.Completed
                },
                new()
                {
                    Title = "Employees",
                    Icon = Icons.Material.Filled.AssignmentInd,
                    Href = "/visitor/employees",
                    PageStatus = PageStatus.Completed
                },
                new()
                {
                    Title = "Visitors",
                    Icon = Icons.Material.Filled.DirectionsRun,
                    Href = "/visitor/Visitors",
                    PageStatus = PageStatus.Completed
                },
                 new()
                {
                    Title = "Pre-registers",
                    Icon = Icons.Material.Filled.Bookmarks,
                    Href = "/visitor/preregisters",
                    PageStatus = PageStatus.Completed
                },
                new()
                {
                    Title = "Histories",
                    Icon = Icons.Material.Filled.History,
                    Href = "/visitor/histories",
                    PageStatus = PageStatus.Completed
                }
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
                    Icon = Icons.Material.Filled.ManageAccounts,
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
