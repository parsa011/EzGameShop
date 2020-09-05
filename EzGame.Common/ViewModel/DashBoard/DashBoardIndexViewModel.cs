using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzGame.Common.ViewModel.DashBoard
{
    public class DashBoardIndexViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
