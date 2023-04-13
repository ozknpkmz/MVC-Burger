using HamburgerMVC.Models.DTOs;

namespace HamburgerMVC.Models.ViewModels
{
    public class MenuVM
    {
        public Menu Menu { get; set; }
        public List<MenuDTO> mList { get; set; }
    }
}
