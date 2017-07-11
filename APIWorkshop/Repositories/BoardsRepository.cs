using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWorkshop.Models;

namespace APIWorkshop.Repositories
{
    public static class BoardsRepository
    {
        public static List<Board> Boards { get; set; }

        static BoardsRepository()
        {
            Boards = new List<Board>
            {
                new Board
                {
                    Id = 1,
                    Name = "First Board",
                    Description = "First Description"
                },
                 new Board
                {
                    Id = 2,
                    Name = "Second Board",
                }
            };
        }

    }
}
