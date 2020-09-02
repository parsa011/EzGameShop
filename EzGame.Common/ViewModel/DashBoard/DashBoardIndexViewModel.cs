using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzGame.Common.ViewModel.DashBoard
{
    public class DashBoardIndexViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<User> users { get; set; }
        public IEnumerable<Comment> comments { get; set; }
    }
}
