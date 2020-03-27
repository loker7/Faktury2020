using System;
using System.Collections.Generic;
using System.Text;

namespace Faktury2020
{
    public class Book
    {
        public string author { get; set; }
        public string title { get; set; }
        public int year { get; set; }
    }

    public class Books : List<Book>
    {
        public Books()
        {
            this.Add(new Book()
            {
                author = "Tomasz",
                title = "Program",
                year = 2015
            });
            this.Add(new Book()
            {
                author = "Magda",
                title = "Program2",
                year = 2016
            });
            this.Add(new Book()
            {
                author = "Józef",
                title = "Program3",
                year = 2019
            });


        }
    }


}
