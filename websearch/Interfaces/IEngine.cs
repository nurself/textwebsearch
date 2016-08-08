using System;
using websearch.Models;

namespace websearch.Interfaces
{
    interface IWebSearchEnginable
    {
        Page Search(String input);
    }
}
